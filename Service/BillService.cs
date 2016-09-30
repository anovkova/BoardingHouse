using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Domain;
using Mappers;
using NHibernate;
using NHibernates.Repositories;
using ViewModels;
using static System.Decimal;

namespace Service
{
    public class BillService
    {
        private IRepository<Bill> _billRepository;
        private IRepository<BillType> _billTypeRepository;
        private IRepository<Rent> _rentRepository;
        private IRepository<Status> _statusRepository;

        public byte[] GenerateBill(BillViewModel model)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _billRepository = new Repository<Bill>(session);
                _billTypeRepository = new Repository<BillType>(session);
                _rentRepository = new Repository<Rent>(session);
                _statusRepository = new Repository<Status>(session);

                if (model.BillType == null)
                    throw new ApplicationException("BILL_TYPE_NOT_FOUND");

                var billType = _billTypeRepository.FindBy(model.BillType.Id);
                if (billType == null)
                    throw new ApplicationException("BILL_TYPE_NOT_FOUND");

                if (model.Rent == null)
                    throw new ApplicationException("RENT_NOT_FOUND");

                var rent = _rentRepository.FindBy(model.Rent.Id);
                if (rent == null)
                    throw new ApplicationException("RENT_NOT_FOUND");

                var status = _statusRepository.FindBy(BillStatusEnum.Draft);
                if(status == null)
                    throw new ApplicationException("STATUS_NOT_FOUND");

                decimal amount;
                TryParse(model.Amount, out amount);
                
                var mergeModel = new BillMergeModel
                {
                    FullName = string.Format("{0} {1}", rent.User.FirstName, rent.User.LastName),
                    Address = rent.User.Address,
                    Amount = amount.ToString("F"),
                    Uin = rent.User.Embg,
                    AccountNumber = billType.AccountNumber,
                    CompanyName = billType.Name,
                    Bank = billType.Bank,
                    ExpenseOfBudgetUser = billType.ExpenseOfBudgetUser,
                    PurposeOfPayment = billType.PurposeOfPayment,
                    RevenueCode = billType.RevenueCode,
                    SuspenseAccount = billType.SuspenseAccount
                };

                var xml = AsposeHelper.SerializeObjectAsXml(mergeModel);
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
                var ds = new DataSet();
                ds.ReadXml(stream);

                var documentContent = AsposeHelper.ExecuteAsposeMerge(
                    new string[0],
                    new object[0],
                    "BillTemplates\\BillTemplate.doc",
                    ds);
                
                var bill = new Bill
                {
                    Amount = model.Amount,
                    Rent = rent,
                    BillType = billType,
                    Status = status,
                    Month = model.Month,
                    BillContent = documentContent
                };

                _billRepository.AddOrUpdate(bill);
                transaction.Commit();
                return documentContent;
            }
        }

        public List<BillTypeViewModel> GetAllBillTypes()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _billTypeRepository = new Repository<BillType>(session);

                return _billTypeRepository.All().Select(x => x.ToModel()).ToList();
            }
        }
    }
}
