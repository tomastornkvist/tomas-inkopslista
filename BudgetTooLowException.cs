class BudgetTooLowException(string message = null)
    : Exception($"Budgeten är för låg.{
        (message != null && message.Length > 0 ? " " + message : "")}")
{

}