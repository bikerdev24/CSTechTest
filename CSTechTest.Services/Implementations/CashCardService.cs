using CSTechTest.Common;
using CSTechTest.Entities;
using CSTechTest.Repository.Interfaces;
using CSTechTest.Services.Interfaces;
using log4net;

namespace CSTechTest.Services.Implementations
{
    public class CashCardService : ICashCardService
    {
        private static readonly ILog log = LogeHelper.GetLogger();
        private readonly ICashCardRepository _cashCardRepository;

        public CashCardService(ICashCardRepository cashCardRepository)
        {
            _cashCardRepository = cashCardRepository;
        }

        public bool IsPinValid(CashCard cashCard)
        {
            log.Info($"Validating pin for cash card with id {cashCard.Id}");
            var pin = _cashCardRepository.GetPin(cashCard.Id);

            if (string.IsNullOrWhiteSpace(pin))
            {
                log.Info($"Invalid pin {cashCard.Pin} entered for card with id {cashCard.Id}");
                return false;
            }

            return pin.Equals(cashCard.Pin);            
        }
    }
}
