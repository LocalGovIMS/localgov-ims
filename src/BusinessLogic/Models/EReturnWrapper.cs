using BusinessLogic.Entities;
using System.Linq;

namespace BusinessLogic.Models
{
    public class EReturnWrapper
    {
        public EReturn EReturn { get; set; }

        public decimal? Amount
        {
            get
            {
                switch ((Enums.EReturnType)EReturn.TypeId)
                {
                    case Enums.EReturnType.Cash:
                        return EReturn.EReturnCashes.Sum(x => x.Total);
                    case Enums.EReturnType.Cheque:
                        return EReturn.EReturnCheques.Sum(x => x.Amount);
                    default:
                        return 0;
                }
            }

        }

        public EReturnWrapper(EReturn eReturn)
        {
            EReturn = eReturn;
        }

        public EReturnWrapper()
        {
        }
    }
}
