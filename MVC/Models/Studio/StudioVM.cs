using AppService.DTOs;

namespace MVC.Models.Studio
{
    public class StudioVM
    {
        public int StudioID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public StudioVM() { }

        public StudioVM(StudioDTO studioDTO)
        {
            StudioID = studioDTO.StudioID;
            Name = studioDTO.Name;
            Description = studioDTO.Description;
        }

    }
}

