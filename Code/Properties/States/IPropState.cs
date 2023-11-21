// Interface for all states making sure that these methods are present
interface IPropState
{
    public void Book(Tenant tenant);
    public void Release(PersonBase person);
    public void GetBookingInfo();
    public void Restore();
}