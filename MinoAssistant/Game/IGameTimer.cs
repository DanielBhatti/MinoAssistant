using System;

namespace MinoAssistant.Game;

public interface IGameTimer
{
    int Period { get; }
    double Acceleration { get; }
    Action Callback { get; }

    void Start();
    void Stop();
    void Restart();
}
