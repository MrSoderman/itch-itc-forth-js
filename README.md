# TRUE ITC FORTH IN 256 LOC v 0.8

* **Author:** Jimmy Söderman
* **Location:** Uppsala, Sweden
* **Date:** October 2026

---

## Overview

This repository contains a true Indirect Threaded Code (ITC) Forth engine and core written in a remarkably compact codebase (active parts JavaScript + Forth Core words = totally 256 LOC) plus modules. Although modeled on a 16-bit architecture, the engine can be easily configured to support 64-bit cells over its flat memory bus.

---

## Key Features

* **Compact & Efficient:** Consists of 35 core primitives (also exists in 26 primitives and a few more).
* **True ITC Architecture:** Implements core Forth primitives, control flow, dictionary management, and an inner/outer interpreter.
* **Graphical Screen:** Features a 128x96 byte color screen (colors 0–15) mapped at `C000h`, complete with hardware-level Bresenham line drawing and a 60 Hz VBLANK loop.
* **Interactive Development Environment:** Includes a built-in text editor, a live REPL console (`OK>` prompt), and execution controls (`RUN`, `STOP`, `CLEAR`).
* **Visual Explorer:** An optional `EXPLORE` tool that lets you inspect the dictionary in real-time, step through execution, and monitor data and return stacks.
* **Inline JavaScript Integration:** Includes optional host API hooks allowing `JS[...]JS` code execution.

---

## The Forth-in-Non-Forth to Forth-in-Forth Journey

This system achieves a true "Forth-in-Forth" (metacircular) state through a precise bootstrap sequence where the outer interpreter is ultimately written in Forth words.

### Boot Sequence
* **Primitives:** Defined in the underlying substrate (JavaScript, assembly, or hardware).
* **Inner Interpreter:** Defined in the underlying substrate (JavaScript, Asm, or Hardware, etc.). This is the well-known `NEXT` loop (referred to here as `STEP`), which traditionally never knows of any compiling or interpretation modes and just does its job blindly.
* **Outer Interpreter Seed:** A temporary seed defined in the underlying substrate used just for a brief moment.
* **Core Words Loading:** Core words are read in using the seed, providing high-level words built from the primitives.
* **Self-Hosting Transition:** Once core words are loaded, there are enough words to express the outer interpreter as high-level Forth words, allowing the temporary seed to be discarded.

The system is now fully Forth-in-Forth, metacircular, and self-hosting.

---

## Architecture & The Virtual Machine (VM)

### 1:1 Architecture
Designed with a clean 1:1 structural layout mirroring low-level procedural code, making it exceptionally simple for JavaScript and web developers to understand, translate, or port directly into assembly, machine code, or BASIC environments.

### The VM Components
* **The Primitives:** The base opcodes of the system.
* **The Inner Interpreter:** Acts like a normal CPU core (`STEP`), completely unaware of states, compiling, or interpretation, executing primitives blindly.
* **The Environment:** The underlying hardware-like operational state containing:
  * `SP`: Data Stack
  * `RP`: Return Stack
  * `IP`: Instruction Pointer
  * `W`: Working Register (ITC-Specific) — required by ITC to track active word addresses for execution handlers like `DOCOL` and `DOCREATE`/`DOES>`.

### PDP-11 Architectural Kinship
True to the spirit of classical minicomputers like the PDP-11, this engine uses a pristine Indirect Threaded Code (ITC) design, explicit hardware-style registers (`IP`, `W`, `SP`, `RP`), and a flat memory model to achieve clean, orthogonal execution both conceptually and in its software implementation.

---

## How to Run

1. Extract the files from your ZIP archive.
2. Double-click or open `itc-baremetal.html` in any modern web browser (Chrome, Firefox, Safari, Edge).
3. No compilation, servers, or local installations are required!

---

## Usage & Controls

* **Editor:** Modify or write your Forth programs directly in the text editor area, then click **RUN** to execute.
* **REPL Console:** Type individual Forth commands directly into the prompt (`OK>`).
* **Explorer:** Click the **EXPLORE** button to open the interactive debugger/inspector view, where you can step through code execution line by line.
* **Shortcuts:** Press `Ctrl + Enter` (or `Cmd + Enter`) to quickly run the program from the editor.