using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Kart{

    void Boost();

    int GetTargetCheckPoint();
    void SetTargetCheckPoint(int IncVal);
    int GetCurrentCheckpoint();
    void SetCurrentCheckpoint(int IncVal);
    float GetCheckPointValue();
    void SetCheckPointValue(float IncVal);

    void SetReadyGo();

    bool GetIsNewLap();
    void SetIsNewLap(bool YesNo);

    string GetName();

    void BecomeStunned();

    Vector3 GetPosition();
}
