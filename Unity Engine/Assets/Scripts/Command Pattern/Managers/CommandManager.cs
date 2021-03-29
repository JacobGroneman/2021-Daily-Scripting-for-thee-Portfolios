using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    #region Singleton
        private static CommandManager _instance;
        public static CommandManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("Command Manager is NULL!");
                }
    
                return _instance;
            }
        }
        #endregion

        private List<ICommand> _commandBuffer =
            new List<ICommand>();
        
    private void Awake()
    { 
        _instance = this;
    }

    #region Manage
        public void AddCommandToBuffer(ICommand command)
            {
                _commandBuffer.Add(command);
            }
        private void ClearCommandBuffer()
            {
                _commandBuffer.Clear();
            }
        private void Done()
            {
                var cubes = GameObject.FindGameObjectsWithTag("Cube");
                    foreach (var cube in cubes)
                    {
                        cube.GetComponent<MeshRenderer>().material.color =
                            Color.white;
                    }
            }
        private void Replay()
        { 
            StartCoroutine(ReplayCommandBuffer());
        }
                IEnumerator ReplayCommandBuffer()
                    {
                        Debug.Log("Running IEnum: ReplayCommandBuffer()");
                        
                        foreach (var command in _commandBuffer)
                        {
                            command.Execute();
                            yield return new WaitForSeconds(1.0f);
                        }
                        
                        Debug.Log("Finished IEnum: ReplayCommandBuffer()");
                    }
        private void Rewind()
        { 
            StartCoroutine(RewindCommandBuffer());
        }
                private IEnumerator RewindCommandBuffer()
                    {
                        Debug.Log("Running IEnum: RewindCommandBuffer()");
                        
                        foreach (var command in Enumerable.Reverse(_commandBuffer))
                        {
                            command.Undo();
                            yield return new WaitForSeconds(1.0f);
                        }
                        
                        Debug.Log("Finished IEnum: RewindCommandBuffer()");
                    }
                    #endregion
}
