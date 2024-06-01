namespace Character.Interfaces
{
    public interface ITriggerCheckable
    {
        bool IsAggro { get; set; }
        bool IsWithinStrikingDistance { get; set; }
        void SetAggroStatus(bool isAggro);
        void SetStrikingDistanceBool(bool isWithinStrikingDistance);
    }
}