# ITCH: True ITC Forth in 256 LOC

A lightweight, bare-metal friendly, and beginner-friendly JavaScript implementation of a Forth system featuring an Indirect Threaded Code (ITC) architecture. Created by Jimmy Söderman (Uppsala, Sweden, October 2026).

## Overview

This project contains a true Indirect Threaded Code (ITC) Forth engine and core written in a remarkably compact codebase (active parts JavaScript + Forth Core words total 256 lines of code) alongside modular extensions. Modeled on a classic 16-bit architecture, the engine operates over a flat memory bus.

## The Forth-in-Non-Forth to Forth-in-Forth Journey

This system achieves a true "Forth-in-Forth" (metacircular) state through a precise bootstrap sequence where the outer interpreter is ultimately written in Forth words:

1. **Primitives:** Defined in the underlying substrate (JavaScript, assembly, or hardware).
2. **Inner Interpreter:** Defined in the underlying substrate. Known as the "NEXT" loop (or `STEP` here), it handles execution blindly without knowledge of compiling or interpretation modes.
3. **Seed (Temporary Outer Interpreter):** A brief, substrate-defined parser used initially to read in core words.
4. **Core Words:** High-level words built from primitives are loaded using the seed.
5. **Self-Hosting Transition:** Once core words are loaded, the outer interpreter is expressed entirely as high-level Forth words, and the temporary seed is discarded.

## 1:1 Architecture

Designed with a clean 1:1 structural layout mirroring low-level procedural code, making it exceptionally simple to understand, translate, or port directly into assembly, machine code, or BASIC environments.

* **SP:** Data Stack
* **RP:** Return Stack
* **IP:** Instruction Pointer
* **W:** Working Register (ITC-Specific tracking register required for word execution handlers like `DOCOL` and `DOES>`)

## Key Features

* **35 Core Primitives:** Essential stack, arithmetic, and control-flow operations.
* **True ITC Architecture:** Full support for core Forth primitives, control flow, dictionary management, and interpreters.
* **Graphical Screen:** 128x96 byte color screen (colors 0–15) mapped at `C000h`, complete with hardware-level Bresenham line drawing and a 60 Hz VBLANK loop.
* **Interactive Development Environment:** Includes a built-in text editor, a live REPL console (`OK>` prompt), and execution controls (`RUN`, `STOP`, `CLEAR`).
* **Visual Explorer:** An optional `EXPLORE` tool that lets you inspect the dictionary in real-time, step through execution, and monitor data and return stacks.
* **Inline JavaScript Integration:** Optional host API hooks allowing `JS[ ... ]JS` code execution.

## How to Run

1. Extract the project files.
2. Open `itc-baremetal.html` in any modern web browser (Chrome, Firefox, Safari, Edge).
3. No compilation, servers, or local installations are required.

## Usage & Controls

* **Editor:** Write or modify Forth programs directly in the text editor area, then click **RUN** to execute. (Shortcut: `Ctrl + Enter` or `Cmd + Enter`).
* **REPL Console:** Type individual Forth commands directly into the prompt (`OK>`).
* **Explorer:** Click the **EXPLORE** button to open the interactive debugger/inspector view to step through execution line by line.

## License

Distributed under the MIT License.