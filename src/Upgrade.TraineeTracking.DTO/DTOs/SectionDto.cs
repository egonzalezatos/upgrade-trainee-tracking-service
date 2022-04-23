using System.Collections.Generic;

namespace Upgrade.TraineeTracking.DTO.DTOs
{

    public class SectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }

        private List<TopicDto> Topics { get; set; }
    }
}