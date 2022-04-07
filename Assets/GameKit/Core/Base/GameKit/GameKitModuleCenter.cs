using System;
using System.Collections.Generic;

namespace GameKit
{
    public static class GameKitModuleCenter
    {
        private static readonly CachedLinkedList<GameKitModule> s_gameKitModules = new CachedLinkedList<GameKitModule>();

        public static void Update(float elapseSeconds, float realElapseSeconds)
        {
            foreach (GameKitModule module in s_gameKitModules)
            {
                module.Update(elapseSeconds, realElapseSeconds);
            }
        }

        // 按优先级关闭模块
        public static void Shutdown()
        {
            for (LinkedListNode<GameKitModule> module = s_gameKitModules.Last; module != null; module = module.Previous)
            {
                module.Value.Shutdown();
            }

            s_gameKitModules.Clear();
            ReferencePool.ClearAll();
        }

        // 根据接口名+反射获得模块
        public static TModule GetModule<TModule>() where TModule : class
        {
            Type interfaceType = typeof(TModule);
            if (!interfaceType.IsInterface)
            {
                throw new GameKitException(string.Format("GameKit module {0} should be used as interface.", interfaceType.FullName));
            }

            string moduleName = string.Format("{0}.{1}", interfaceType.Namespace, interfaceType.Name.Substring(1));
            Type moduleType = Type.GetType(moduleName);

            if (moduleType == null)
            {
                throw new GameKitException(string.Format("GameKit module '{0}' is not exist.", moduleName));
            }
            return GetModule(moduleType) as TModule;
        }

        private static GameKitModule GetModule(Type moduleType)
        {
            foreach (GameKitModule module in s_gameKitModules)
            {
                if (module.GetType() == moduleType)
                {
                    return module;
                }
            }

            return CreateModule(moduleType);
        }

        // 创建时按优先级排序
        private static GameKitModule CreateModule(Type moduleType)
        {
            GameKitModule module = (GameKitModule)Activator.CreateInstance(moduleType);
            if (module == null)
            {
                throw new GameKitException(string.Format("GameKit Module {0} can not be created.", moduleType.FullName));
            }

            LinkedListNode<GameKitModule> currentModule = s_gameKitModules.First;
            while (currentModule != null)
            {
                if (module.Priority > currentModule.Value.Priority)
                {
                    break;
                }
                currentModule = currentModule.Next;
            }

            if (currentModule != null)
            {
                s_gameKitModules.AddBefore(currentModule, module);
            }
            else
            {
                s_gameKitModules.AddLast(module);
            }

            return module;
        }
    }
}