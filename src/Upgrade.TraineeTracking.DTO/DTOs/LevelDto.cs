namespace Upgrade.TraineeTracking.DTO.DTOs
{
    public class LevelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public StatusDto Status { get; set; }
    }
}