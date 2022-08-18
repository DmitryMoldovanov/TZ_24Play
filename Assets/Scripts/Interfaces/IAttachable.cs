using Assets.Scripts.Cube;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IAttachable
    {
        public void Attach(CubeHolder cubeHolder);
        public void DeAttach();
    }
}
