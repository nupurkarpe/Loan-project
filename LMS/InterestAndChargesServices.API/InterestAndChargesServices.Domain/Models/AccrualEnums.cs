namespace InterestAndChargesServices.Domain.Models
{
    public enum AccrualType { Regular, Penal, Compound }
    public enum AccrualStatus { Posted, Pending, Reversed }
    public enum CalculationMethod { Flat, Reducing, Compound }
}
