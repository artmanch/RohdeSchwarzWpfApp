namespace ControlLibrary.Services.Interfaces
{
    public interface IParser
    {
        public (double frequency, Metrics metric) Parse(string value);
    }

    public enum Metrics
    {
        Hz,
        kHz,
        MHz
    }
}
