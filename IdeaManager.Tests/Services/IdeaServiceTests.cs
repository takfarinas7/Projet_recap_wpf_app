using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using IdeaManager.Core.Entities;
using IdeaManager.Core.Enums;
using IdeaManager.Core.Interfaces;
using IdeaManager.Services.Services;

namespace IdeaManager.Tests.Services
{
    public class IdeaServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IRepository<Idea>> _repoMock;
        private readonly IdeaService _service;

        public IdeaServiceTests()
        {
            // 1) On crée un Fake IRepository<Idea>
            _repoMock = new Mock<IRepository<Idea>>();

            // 2) On crée un Fake IUnitOfWork qui expose notre repo
            _uowMock = new Mock<IUnitOfWork>();
            _uowMock
                .Setup(u => u.IdeaRepository)
                .Returns(_repoMock.Object);

            // 3) On simule SaveChangesAsync() pour renvoyer "1" (succès)
            _uowMock
                .Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            // 4) On instancie le service réel en lui passant notre Fake UoW
            _service = new IdeaService(_uowMock.Object);
        }

        [Fact]
        public async Task SubmitIdeaAsync_EmptyTitle_ThrowsArgumentException()
        {
            // Arrange: idée invalide (titre vide)
            var badIdea = new Idea
            {
                Title = "",
                Description = "n'importe quoi"
            };

            // Act & Assert: l'appel doit jeter ArgumentException
            await Assert.ThrowsAsync<ArgumentException>(
                () => _service.SubmitIdeaAsync(badIdea)
            );

            // Et on ne doit JAMAIS appeler AddAsync ni SaveChangesAsync
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Idea>()), Times.Never);
            _uowMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task SubmitIdeaAsync_ValidIdea_CallsAddAndSaveAndSetsDefaults()
        {
            // Arrange: idée valide
            var goodIdea = new Idea
            {
                Title = "Mon idée",
                Description = "Une super description"
            };

            // Act: on soumet l'idée
            await _service.SubmitIdeaAsync(goodIdea);

            // Assert 1: AddAsync a bien été appelé UNE fois AVEC le même objet
            _repoMock.Verify(r =>
                r.AddAsync(It.Is<Idea>(i =>
                    i.Title == goodIdea.Title &&
                    i.Description == goodIdea.Description
                )),
                Times.Once
            );

            // Assert 2: SaveChangesAsync a bien été appelé UNE fois
            _uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);

            // Assert 3: le service a correctement initialisé VoteCount et Status
            Assert.Equal(0, goodIdea.VoteCount);
            Assert.Equal(IdeaStatus.InProgress, goodIdea.Status);
        }
    }
}
