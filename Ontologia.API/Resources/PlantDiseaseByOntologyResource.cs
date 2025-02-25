﻿namespace Ontologia.API.Resources;

public class PlantDiseaseByOntologyResource
{
    public string InfeccionId { get; set; }
    public string NombreComun { get; set; }
    public string Tipo { get; set; }
    public string NombreCientifico { get; set; }
    public string Descripcion { get; set; }
    public decimal? AfectaA { get; set; }
    public IEnumerable<SintomaDataResource> Sintomas { get; set; }
    public IEnumerable<AgenteCausalDataResource> AgentesCausales { get; set; }
}