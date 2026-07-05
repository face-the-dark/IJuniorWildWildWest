using Cysharp.Threading.Tasks;

namespace Game.Infrastructure.Loading
{
    public interface ISceneLoader
    {
        public UniTask Load(string sceneName);
    }
}