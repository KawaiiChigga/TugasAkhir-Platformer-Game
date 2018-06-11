/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class TanpaState
    {
        public void Update()
        {
            // Check if Player is touching a ground
            Checkground = CheckTouch("Ground");

            if (Checkground)
            {
                isGrounded = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            { // Jump Key
                if (isGrounded && !isDashing)
                {
                    Jump();
                }
            }

            if (Player.getHit())
            {
                if (!isInvulnerable)
                {
                    isDamaged = true;
                }
            }

            if (isDamaged)
            {
                isInvulnerable = true;
                setColor(red);
                setTimer(3);
                isDamaged = false;
                IsInvulnerable = false;
                setColor(white);
            }
        }
    }
}
*/