namespace BalazsGarazs.Api.Data.Shared.Interfaces;

public interface ISoftDeleteItem
{
    bool IsDeleted { get; set; }
    DateTime? DeletedAt { get; set; }
}
