namespace Poprawa.DTOs;


public class NewArtifactProjectDto
{
    public NewArtifactDto Artifact { get; set; }
    public NewProjectDto Project { get; set; }
}

public class NewArtifactDto
{
    public int ArtifactId { get; set; }
    public string Name { get; set; }
    public DateTime OriginDate { get; set; }
    public int InstitutionId { get; set; }
}

public class NewProjectDto
{
    public int ProjectId { get; set; }
    public string Objective { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

