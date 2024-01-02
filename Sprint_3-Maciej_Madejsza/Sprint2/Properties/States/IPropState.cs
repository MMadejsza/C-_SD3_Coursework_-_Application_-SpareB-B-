// Interface for all states making sure that these methods are present
interface IPropState
{
    public string Book(Tenant tenant);
    public string Release(PersonBase person);
    public string GetBookingInfo();
    public string Restore();
}