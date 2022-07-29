﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameKit.Timer
{
    public abstract class Ticker
    {
        /// <summary>
        /// 当前执行次数
        /// </summary>
        public int nowExecuteTime;
        /// <summary>
        /// 目标执行次数
        /// </summary>
        public int targetExecuteTime;
        /// <summary>
        /// 剩余执行次数
        /// </summary>
        public int remainingExecuteTime
        {
            get => isLoop ? -1 : targetExecuteTime - nowExecuteTime;
        }
        public bool isPause { get; protected set; }
        public bool isFinish { get; protected set; }
        public bool isLoop;

        public Action onTick;
        public Action onPause;
        public Action onResume;
        public Action onCancel;
        public Action onFinish;

        public abstract void Start();
        public abstract void DoTick();
        public abstract void Pause();
        public abstract void Resume();
        public abstract void Cancel();
    }

    /// <summary>
    /// 自动计数执行器
    /// </summary>
    public class Ticker_Auto : Ticker
    {
        private float _interval;
        /// <summary>
        /// 执行间隔
        /// </summary>
        public float Interval
        {
            get => _interval;
            set
            {
                if (value == _interval) return;
                _interval = value;
                if (_timer == null) return;
                _timer.Cancel();
                _timer = Timer.Register(_interval, DoTick);
                _timer.isLooped = true;
            }
        }
        /// <summary>
        /// 当前计数
        /// </summary>
        public float nowTicks
        {
            get => _timer == null ? -1 : _timer.GetTimeElapsed() + nowExecuteTime * Interval;
        }
        /// <summary>
        /// 目标计数
        /// </summary>
        public float targetTicks
        {
            get => isLoop ? -1 : targetExecuteTime * Interval;
        }
        /// <summary>
        /// 剩余计数
        /// </summary>
        public float remainingTicks
        {
            get
            {
                if (_timer == null) return targetTicks;
                if (isLoop) return -1;
                return _timer.GetTimeRemaining() + remainingExecuteTime * Interval;
            }
        }

        private Timer _timer;

        /// <summary>
        /// 循环自动计数执行器
        /// </summary>
        /// <param name="interval">执行间隔(秒)</param>
        public Ticker_Auto(float interval)
        {
            isLoop = true;
            isPause = true;
            isFinish = false;
            this.Interval = interval;
            targetExecuteTime = -1;
        }
        /// <summary>
        /// 自动计数执行器
        /// </summary>
        /// <param name="targetExecuteTime">目标执行数(次数)</param>
        /// <param name="interval">执行间隔(秒)</param>
        public Ticker_Auto(int targetExecuteTime, float interval)
        {
            isLoop = false;
            isPause = true;
            isFinish = false;
            this.Interval = interval;
            this.targetExecuteTime = targetExecuteTime;
        }

        public override void Start()
        {
            isPause = false;
            isFinish = false;
            nowExecuteTime = 0;

            _timer = Timer.Register(Interval, DoTick);
            _timer.isLooped = true;
        }

        public override void DoTick()
        {
            if (isPause || isFinish) return;
            onTick?.Invoke();
            nowExecuteTime++;
            if (!isLoop && nowExecuteTime == targetExecuteTime)
            {
                onFinish?.Invoke();
                _timer.Cancel();
                isPause = true;
                isFinish = true;
            }
        }

        public override void Pause()
        {
            if (isFinish || isPause) return;
            isPause = true;
            _timer.Pause();
            onPause?.Invoke();
        }

        public override void Resume()
        {
            if (isFinish || !isPause) return;
            isPause = false;
            _timer.Resume();
            onResume?.Invoke();
        }

        public override void Cancel()
        {
            _timer.Cancel();
            isPause = true;
            isFinish = true;
        }

        public void Register(Action onUpdate) => onTick += onUpdate;

    }

    /// <summary>
    /// 手动计数执行器
    /// </summary>
    public class Ticker_Manual : Ticker
    {
        private int _interval;
        /// <summary>
        /// 执行间隔
        /// </summary>
        public int Interval
        {
            get => _interval;
            set
            {
                if (value == _interval) return;
                _interval = value;
            }
        }
        private int _ticks;

        /// <summary>
        /// 当前计数
        /// </summary>
        public int nowTime
        {
            get => _ticks + nowExecuteTime * _interval;
        }
        /// <summary>
        /// 目标计数
        /// </summary>
        public float targetTime
        {
            get => isLoop ? -1 : targetExecuteTime * _interval;
        }
        /// <summary>
        /// 剩余计数
        /// </summary>
        public float remainingTime
        {
            get
            {
                if (isLoop) return -1;
                return _ticks + remainingExecuteTime * _interval;
            }
        }

        /// <summary>
        /// 循环手动计数执行器
        /// </summary>
        /// <param name="interval">执行间隔(次数)</param>
        public Ticker_Manual(int interval)
        {
            isLoop = true;
            isPause = true;
            isFinish = false;
            _interval = interval;
            targetExecuteTime = -1;
        }

        /// <summary>
        /// 手动计数执行器
        /// </summary>
        /// <param name="targetExecuteTime">目标执行数(次数)</param>
        /// <param name="interval">执行间隔(次数)</param>
        public Ticker_Manual(int targetExecuteTime, int interval)
        {
            isLoop = false;
            isPause = true;
            isFinish = false;
            _interval = interval;
            this.targetExecuteTime = targetExecuteTime;
        }

        public override void Start()
        {
            isPause = false;
            isFinish = false;
            _ticks = 0;
            nowExecuteTime = 0;
        }
        public override void DoTick()
        {
            if (isPause || isFinish) return;
            _ticks++;
            if (_ticks != _interval) return;
            onTick?.Invoke();
            nowExecuteTime++;
            _ticks = 0;
            if (!isLoop && nowExecuteTime == targetExecuteTime)
            {
                onFinish?.Invoke();
                isPause = true;
                isFinish = true;
            }
        }
        public override void Pause()
        {
            if (isFinish || isPause) return;
            isPause = true;
            onPause?.Invoke();
        }
        public override void Resume()
        {
            if (isFinish || !isPause) return;
            isPause = false;
            onResume?.Invoke();
        }
        public override void Cancel()
        {
            isPause = true;
            isFinish = true;
        }
    }
}
