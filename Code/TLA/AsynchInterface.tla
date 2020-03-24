-------------------------- MODULE AsynchInterface --------------------------
EXTENDS Naturals
CONSTANTS Data
VARIABLES value, ready, acknowledge
TypeInvariant == /\ value \in Data
                 /\ ready \in {0,1}
                 /\ acknowledge \in {0,1}                 
-----------------------------------------------------------------------------                 
Init == /\ value \in Data
        /\ ready \in {0,1}
        /\ acknowledge = ready
        
Send == /\ ready = acknowledge
        /\ value' \in Data
        /\ ready' = 1 - ready
        /\ UNCHANGED acknowledge
        
Receive == /\ ready /= acknowledge
           /\ acknowledge' = 1 - acknowledge
           /\ UNCHANGED <<value, ready>>
              
Next == Send \/ Receive
Spec == Init /\ [][Next]_<<value, ready, acknowledge>>
-----------------------------------------------------------------------------
THEOREM Spec => []TypeInvariant
=============================================================================