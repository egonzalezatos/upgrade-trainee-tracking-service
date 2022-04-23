namespace Upgrade.TraineeTracking.DTO.DTOs
{
    public class JobProfileDto
    {
        public int Id { get; set; }
        public JobPositionDto JobPosition { get; set; }
        public TechnologyDto Technology { get; set; }
        public LevelDto Level { get; set; }
    }
}