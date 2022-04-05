﻿namespace Ontologia.API.Domain.Models
{
    public class PlantDisease
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with CategoryDisease Entity
        public Guid? CategoryDiseaseId { get; set; }
        public CategoryDisease? CategoryDisease { get; set; }

    }
}