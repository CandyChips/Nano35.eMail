using System.Threading;
using System.Threading.Tasks;

namespace Nano35.eMail.Processor.Requests
{
    public interface IPipelineNode<TIn, TOut>
    {
        Task<TOut> Ask(TIn input, CancellationToken cancellationToken);
    }
} 