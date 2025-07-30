using Backend.DTOs;
using Backend.Http;

namespace Backend.Queries
{
    public record GetFonManifestationQuery : BaseQuery<Result<ManifestationDto>>;
}
