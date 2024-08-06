using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX
{
    public class MulticopterModel : ModelSystem
    {
        public RBModel rbModel;
        public Adder adder;

        protected void Awake()
        {
            if (TryGetComponent(out RBModel rbModel))
            {
                this.rbModel = rbModel;
            }

            if (TryGetComponent(out Adder adder))
            {
                this.adder = adder;
            }
        }

        public MulticopterModel()
        {

        }
    }
}
