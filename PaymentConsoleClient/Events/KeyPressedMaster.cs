using System;

namespace PaymentConsoleClient.Events;

public class KeyPressedMaster
{
    public event EventHandler? KeyPressed;

    public void OnKeyDown(char keyCode)
    {
        KeyPressed?.Invoke(this, EventArgs.Empty);
        
    }
}