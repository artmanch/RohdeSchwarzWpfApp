namespace ControlLibrary.Services.Interfaces
{
    public interface IParser
    {
        public (long frequency, Metrics metric) Parse(string value);
    }

    public enum Metrics
    {
        Hz,
        kHz,
        MHz
    }
}
