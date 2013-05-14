﻿using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Mono.Cecil;

namespace Commander.Fody
{
    public class ModuleTypesProcessor : ModuleProcessorBase
    {
        public ModuleTypesProcessor([NotNull] ModuleWeaver moduleWeaver) : base(moduleWeaver)
        {
        }

        public override void Execute()
        {
            ProcessTypes(GetTypesToProcess());
        }

        public virtual IEnumerable<TypeDefinition> GetTypesToProcess()
        {
            return ModuleWeaver.GetTypes().Where(x => x.IsClass);
        }

        public void ProcessTypes(IEnumerable<TypeDefinition> types)
        {
            foreach (var type in types)
            {
                try
                {
                    var typeProcessor = new TypeProcessor(type, ModuleWeaver);
                    typeProcessor.Execute();
                }
                catch (Exception ex)
                {
                    Assets.Log.Error(ex);
                }
            }
        }        
    }
}