---
theme: "solarized"
transition: "slide"
highlightTheme: "darkula"
slideNumber: true
---

# Practical Messaging

### Presentation starts ~ 12:02


---

# Practical Messaging
* Demo message driven batch (ServicePulse)
* Demo event driven order system (ServiceInsight)
* Obstacles in message driven systems
* Demo dependant invoice generation system

---

## Message based batch processing (Demo)

--

![](importer1.jpg)

--

![](importer2.jpg)

---

## Event driven ordersystem (Demo)

--

![](order1.jpg)

---

## Transport transactions 
### At-least-once delivery

--

### Receiving messages

![](msg1.jpg)

--

Message handler logic will be executed\
 *at-least once* per message on the queue

--

### Sending messages

![](msg2.jpg)

--

Messages can be duplicated when sent to different queues

--

### Atomic operations
* All IO operations must be committed as one
* Fail or complete together

--

#### Forwarding

![](msg3.jpg)

---

### Partial completion

--

 * Operations targeting multiple storage types
 * Not possible to execute atomically
 * E.g. queue + SQL

--

![](msg4.jpg)

--

Parts of message handler executed before failure,\
logic possibly executed twice

--

#### Outbox

![](msg5.jpg)

--

Duplicates can still occur!

---

### Idempotency

--

#### Natural idempotency

* TurnLightSwitch
* SwitchOfTheLights

---

#### De-duplication

![](msg6.jpg)

---

### NServiceBus outbox
* Implementation of outbox + de-dup for SQL server
* Ensures exactly-once delivery

--

![](nsb-outbox.svg)

--

### Message ordering
* No guarantee on message order

--


![](ordering1.jpg)

--

* No magic solution
* Never asume ordered message delivery
* Avoid dependant messages

---

## Dependant invoice generation system (Demo)

--

![](invoice1.jpg)

--

![](invoice2.jpg)

---


