# TRUE ITC FORTH IN 256 LOC v 0.8[cite: 1]

* **Author:** Jimmy Söderman[cite: 1]
* **Location:** Uppsala, Sweden[cite: 1]
* **Date:** October 2026[cite: 1]

---

## Overview[cite: 1]

This repository contains a true Indirect Threaded Code (ITC) Forth engine and core written in a remarkably compact codebase (active parts JavaScript + Forth Core words = totally 256 LOC) plus modules[cite: 1]. Although modeled on a 16-bit architecture, the engine can be easily configured to support 64-bit cells over its flat memory bus[cite: 1].

---

## Key Features[cite: 1]

* **Compact & Efficient:** Consists of 35 core primitives (also exists in 26 primitives and a few more)[cite: 1].
* **True ITC Architecture:** Implements core Forth primitives, control flow, dictionary management, and an inner/outer interpreter[cite: 1].
* **Graphical Screen:** Features a 128x96 byte color screen (colors 0–15) mapped at `C000h`, complete with hardware-level Bresenham line drawing and a 60 Hz VBLANK loop[cite: 1].
* **Interactive Development Environment:** Includes a built-in text editor, a live REPL console (`OK>` prompt), and execution controls (`RUN`, `STOP`, `CLEAR`)[cite: 1].
* **Visual Explorer:** An optional `EXPLORE` tool that lets you inspect the dictionary in real-time, step through execution, and monitor data and return stacks[cite: 1].
* **Inline JavaScript Integration:** Includes optional host API hooks allowing `JS[...]JS` code execution[cite: 1].

---

## The Forth-in-Non-Forth to Forth-in-Forth Journey[cite: 1]

This system achieves a true "Forth-in-Forth" (metacircular) state through a precise bootstrap sequence where the outer interpreter is ultimately written in Forth words[cite: 1].

### Boot Sequence[cite: 1]
* **Primitives:** Defined in the underlying substrate (JavaScript, assembly, or hardware)[cite: 1].
* **Inner Interpreter:** Defined in the underlying substrate (JavaScript, Asm, or Hardware, etc.). This is the well-known `NEXT` loop (referred to here as `STEP`), which traditionally never knows of any compiling or interpretation modes and just does its job blindly[cite: 1].
* **Outer Interpreter Seed:** A temporary seed defined in the underlying substrate used just for a brief moment[cite: 1].
* **Core Words Loading:** Core words are read in using the seed, providing high-level words built from the primitives[cite: 1].
* **Self-Hosting Transition:** Once core words are loaded, there are enough words to express the outer interpreter as high-level Forth words, allowing the temporary seed to be discarded[cite: 1].

The system is now fully Forth-in-Forth, metacircular, and self-hosting[cite: 1].

---

## Architecture & The Virtual Machine (VM)[cite: 1]

### 1:1 Architecture[cite: 1]
Designed with a clean 1:1 structural layout mirroring low-level procedural code, making it exceptionally simple for JavaScript and web developers to understand, translate, or port directly into assembly, machine code, or BASIC environments[cite: 1].

### The VM Components[cite: 1]
* **The Primitives:** The base opcodes of the system[cite: 1].
* **The Inner Interpreter:** Acts like a normal CPU core (`STEP`), completely unaware of states, compiling, or interpretation, executing primitives blindly[cite: 1].
* **The Environment:** The underlying hardware-like operational state containing[cite: 1]:
  * `SP`: Data Stack[cite: 1]
  * `RP`: Return Stack[cite: 1]
  * `IP`: Instruction Pointer[cite: 1]
  * `W`: Working Register (ITC-Specific) — required by ITC to track active word addresses for execution handlers like `DOCOL` and `DOCREATE`/`DOES>`[cite: 1].

### PDP-11 Architectural Kinship[cite: 1]
True to the spirit of classical minicomputers like the PDP-11, this engine uses a pristine Indirect Threaded Code (ITC) design, explicit hardware-style registers (`IP`, `W`, `SP`, `RP`), and a flat memory model to achieve clean, orthogonal execution both conceptually and in its software implementation[cite: 1].

---

## How to Run[cite: 1]

1. Extract the files from your ZIP archive[cite: 1].
2. Double-click or open `itc-baremetal.html` in any modern web browser (Chrome, Firefox, Safari, Edge)[cite: 1].
3. No compilation, servers, or local installations are required![cite: 1]

---

## Usage & Controls[cite: 1]

* **Editor:** Modify or write your Forth programs directly in the text editor area, then click **RUN** to execute[cite: 1].
* **REPL Console:** Type individual Forth commands directly into the prompt (`OK>`)[cite: 1].
* **Explorer:** Click the **EXPLORE** button to open the interactive debugger/inspector view, where you can step through code execution line by line[cite: 1].
* **Shortcuts:** Press `Ctrl + Enter` (or `Cmd + Enter`) to quickly run the program from the editor[cite: 1].