using R3;

namespace LOGIC.Money
{
    public interface IMoneyHandler
    {
        public void AddMoney(float addValue);
        public bool TrySubtractMoney(int subtractValue);
    }
}