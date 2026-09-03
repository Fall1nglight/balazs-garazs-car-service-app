using BalazsGarazs.Api.Data.Shared.Interfaces;

namespace BalazsGarazs.Api.Common.Pagination;

public interface IOffsetItem : IEntity
{
    public DateTime CreatedAt { get; }
}
