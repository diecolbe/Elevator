namespace com.device.Interfaces
{
    public interface IElevator
    {
        void OnElevator();
        void UpElevator(Floor floor);
        void DownElevator(Floor floor);
        void StopElevator();
    }
}
