using System;
using System.Data;
using System.IO;
using System.Text;
using Domain;
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
        
        public byte[] GenerateBill(BillViewModel model)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (session.BeginTransaction())
            {
                _billRepository = new Repository<Bill>(session);
                _billTypeRepository = new Repository<BillType>(session);
                _rentRepository = new Repository<Rent>(session);

                //if (model.BillType == null)
                //    throw new ApplicationException("BILL_TYPE_NOT_FOUND");

                //var billType = _billTypeRepository.FindBy(model.BillType.Id);
                //if(billType == null)
                //    throw new ApplicationException("BILL_TYPE_NOT_FOUND");

                //if(model.Rent == null)
                //    throw new ApplicationException("RENT_NOT_FOUND");

                //var rent = _rentRepository.FindBy(model.Rent.Id);
                //if (rent == null)
                //    throw new ApplicationException("RENT_NOT_FOUND");
                
                decimal amount;
                TryParse(model.Amount, out amount);

                var mergeModel = new BillMergeModel
                {
                    FullName = "Ристо Панчевски",//String.Format("{0} {1}", rent.User.FirstName, rent.User.LastName),
                    Address = "ул. Исаија Мажовски",//rent.User.Address,
                    Amount = amount.ToString("F"),
                    Uin = "2007989432003",
                    Bank = "НБРМ",
                    AccountNumber = "100000000061025",
                    CompanyName = "ЕВН",
                    ExpenseOfBudgetUser = "100000000061025",
                    PurposeOfPayment = "100000000061025",
                    RevenueCode = "100000000061025",
                    SuspenseAccount = "100000000061025"
                };

                var xml = AsposeHelper.SerializeObjectAsXml(mergeModel);
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
                var ds = new DataSet();
                ds.ReadXml(stream);

                var documentContent = AsposeHelper.ExecuteAsposeMerge(
                    new string[0],
                    new object[0],
                    "BillTemplates\\EVN.doc",
                    ds);

                //TODO: save bill in database with generated content

                return documentContent;
            }
        }
    }
}
