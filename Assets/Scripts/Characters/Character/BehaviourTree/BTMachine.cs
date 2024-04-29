using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMachine
{
   private IBTNode _rootNode;

   public BTMachine(IBTNode rootNode)
   {
      _rootNode = rootNode;
   }

   public void Operate()
   {
      _rootNode.Evaluate();
   }
}
