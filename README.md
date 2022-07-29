# A Unity Kit For Quick Game Dev

### Last Update

2022/07/29

### Core

- Base
  - 单例 ``SingletonBase``
  - Mono单例  `` MonoSingletonBase``
  - 命令 ``Command``
  - UI组件 ``UIForm`` ``UIGroup``

- Data Structure
  - 树和节点 ``Tree``
  - 缓存链表 ``CachedLinkedList``
  - 限度链表 ``LinkedListRange``
  - 多值字典 ``MultiDictionary``
  - 缓存字典 ``QueueDictionary``
  - 环形缓冲 ``RingBuffer``
  - 序列化链表和序列化字典 ``Serialization``
  - 类名对主键 ``TypeNamePair``

- GameKit
  - GameKit相关基础设计，基本结构模仿GameFramework，做了删减

- Helper（辅助器相关）
- Manager （用于快速访问的单例管理器）
  - ``AudioManager`` 音效管理器
  - ``EventManager`` 基于泛型的事件中转
  - ``InputManager`` 输入管理器，基于``Input``重封装
  - ``PoolManager`` 面向``GameObject``的快速缓冲池
  - ``ResourceManager`` 基于``Resource``和``Addressable``的快速资源管理器
  - ``ScenesManager`` 场景管理器，基于``SceneManagement`` 重封装
  - ``SerializeManager`` 快速序列化器，支持``Json``和二进制
  - ``UIManager`` 注册时的UI管理器

- Prefab （放置于Launcher中的Kit核心预制体，模仿GF）

### 其他模块

1. ``DataTable``简易 Excel 配表工具
2. ``Entity``实体管理模块
3. ``Fsm`` 有限状态机模块
4. ``Dialog`` 支持语法控制的对话系统
5. ``ReferencePool`` 引用池
6. ``ObjectPool`` 对象池
7. ``Functional`` 独立组件
8. ``Procedure`` 游戏流程控制
9. ``Timer`` 计时器
10. ``Utilities`` 通用
11. ``Externals`` 常用外部中间件
12. ``Legacy`` 历史代码暂时没删

### Reference

[GameFramework](https://github.com/EllanJiang/GameFramework)

[MycroftToolkit](https://github.com/MycroftCooper/MycroftToolkit)

待补充
