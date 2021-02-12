using Kros.CqrsTemplate.Domain;
using Kros.KORM;
using Kros.Utils;
using System.Threading.Tasks;

namespace Kros.CqrsTemplate.Infrastructure
{
    /// <summary>
    /// Repository for persistating <see cref="RRREntityNameRRR_"/>.
    /// </summary>
    public class RRREntityNameRRR_Repository : IRRREntityNameRRR_Repository
    {
        private readonly IDatabase _database;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="database">Database</param>
        public RRREntityNameRRR_Repository(IDatabase database)
        {
            _database = Check.NotNull(database, nameof(database));
        }

        /// <inheritdoc />
        public async Task CreateRRREntityNameRRR_Async(RRREntityNameRRR_ item)
            => await _database.AddAsync(item);

        /// <inheritdoc />
        public async Task UpdateRRREntityNameRRR_Async(RRREntityNameRRR_ item)
            => await _database.EditAsync(item);

        /// <inheritdoc />
        public async Task DeleteRRREntityNameRRR_Async(long id)
            => await _database.DeleteAsync<RRREntityNameRRR_>(id);
    }
}
