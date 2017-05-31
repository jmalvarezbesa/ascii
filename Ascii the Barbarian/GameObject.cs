using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class GameObject
    {
        public char symbol;
        public int[] position;
        public double[] positionBuffer;
        public int[] velocity;
        public string tag;
        public bool existCollition = false;

        public IControllerComponent controlCom;
        public IPhysicsComponent physicsCom;
        public IAudioComponent audioCom;
        public IGraphicsComponent graphicsCom;

        public GameObject (IControllerComponent controlCom, IPhysicsComponent physicsCom, IGraphicsComponent graphicsCom, IAudioComponent audioCom)
        {
            this.controlCom = controlCom;
            this.physicsCom = physicsCom;
            this.audioCom = audioCom;
            this.graphicsCom = graphicsCom;
            this.symbol = graphicsCom.GetSymbol();
            this.velocity = new int[]{ 0, 0};
            this.positionBuffer = new double[] { 0.0, 0.0};
        }
        public void Start(int[] position, string tag)
        {
            this.position = position;
            this.tag = tag;
        }
        public int[] GetPosition()
        {
            return position;
        }

        public char GetSymbol()
        {
            return symbol;
        }

        public void Update(List<GameObject> gameObjects, char graphics, movement command)
        {
            controlCom.Update(this, command);
            physicsCom.Update(this, gameObjects);
            audioCom.Update(this);
            graphicsCom.Update(this, graphics);
        }
    }
}
