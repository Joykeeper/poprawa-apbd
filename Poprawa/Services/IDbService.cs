using Poprawa.DTOs;

namespace Poprawa.Services;

public interface IDbService
{
    Task<ProjectDto> GetProjectInfo(int id);
    Task AddArtifact(NewArtifactProjectDto artifactProject);
}