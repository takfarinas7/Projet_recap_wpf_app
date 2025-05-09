using IdeaManager.Core.Entities;
using IdeaManager.Core.Interfaces;

public class IdeaService : IIdeaService
{
    private readonly IUnitOfWork _unitOfWork;

    public IdeaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SubmitIdeaAsync(Idea idea)
    {
        if (string.IsNullOrWhiteSpace(idea.Title))
            throw new ArgumentException("Le titre est obligatoire.");

        idea.Votes = 0;
        idea.IsApproved = false;

        await _unitOfWork.IdeaRepository.AddAsync(idea);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<Idea>> GetAllAsync()
    {
        return await _unitOfWork.IdeaRepository.GetAllAsync();
    }

    public Task VoteForIdeaAsync(int ideaId)
    {
        throw new NotImplementedException();
    }
}
