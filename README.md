# ITCH

**A real, authentic Indirect Threaded Code (ITC) Forth, written in simple JavaScript, small enough to read in one sitting.**

* **Author:** Jimmy Söderman
* **Location:** Uppsala, Sweden
* **Date:** October 2026

![ITCH running in the browser](docs/learnForth.png)

---

## What is this?

ITCH is a true ITC Forth whose active parts (JavaScript engine + Forth core words) fit in **128–256 lines of code**.

The idea is Alan Kay's: he described the half page of LISP that defines LISP itself as the "Maxwell's equations of software". ITCH tries to do the same for Forth. It aims for a small core where you can see everything, and everything else is built from it.

**JavaScript is the BASIC of today.** Everyone has it, it runs everywhere, and you need nothing but a browser. So the engine is written in plain, flat JavaScript that maps 1:1 to low-level code. You could translate it line by line into assembly, machine code or BASIC.

**Simple ITC in simple source code.**

---

## Why?

* **To teach Forth.** It shows how Forth really works, starting from the bare machine.
* **To be a proper Forth.** It's not a "fake Forth" that only looks like Forth on the surface. Everything builds everything, like LEGO.
* **To stay bare-metal.** It has no objects and no `EVALUATE` tricks. Registers, memory and stacks are laid out the way a real machine would have them.

The best building blocks in Forth do the heavy lifting here:

| Word pair | What it gives you |
|-----------|-------------------|
| `CREATE ... DOES>` | New kinds of words: defining words that make defining words |
| `DEFER ... IS` | Late binding: change behaviour after the fact, without rewriting code |

ITCH covers these properly, in a minimum of code.

---

## The three steps

The project is a learning path. Each step builds on the one before.

```
  1. Forth-in-Stone   →   2. True ITC   →   3. ITCH
     (learn)                (understand)        (experiment)
```

### 1. Forth-in-Stone: learn how Forth works

📁 `forth-in-stone/`

This is a tiny Forth with very few words, built around `CREATE DOES>` and `DEFER IS`. It comes with a **PDF guide** that walks you through how Forth works and how it grows its magic. The interpreter comes first. Then the compiler is written in Forth itself.

Start here if you are new to Forth.

### 2. True ITC: a complete, classic ITC Forth

📁 `true-itc/`

This is a complete Indirect Threaded Code Forth in about 256 lines, modeled on a 16-bit machine.

* **35 core primitives.** There is also a 26-primitive variant.
* **Real ITC machine.** It has explicit registers `IP`, `W`, `SP` and `RP` and a flat memory bus. It can be configured for 64-bit cells.
* **Inner interpreter.** `STEP`, the classic `NEXT` loop, works like a CPU core. It knows nothing about compiling or interpreting. It just executes.
* **Graphics.** It has a 128×96 screen with 16 colors mapped at `C000h`, hardware-style Bresenham line drawing and a 60 Hz VBLANK loop.
* **IDE.** It includes an editor, a live REPL (`OK>`), and `RUN` / `STOP` / `CLEAR` controls.
* **EXPLORE.** This optional visual debugger lets you inspect the dictionary, step through execution and watch both stacks.
* **Inline JavaScript.** Optional `JS[ ... ]JS` hooks let you call the host.

#### From Forth-in-JavaScript to Forth-in-Forth

The system boots its way into being metacircular:

1. **Primitives** are written in the substrate (JavaScript here; assembly or hardware elsewhere).
2. **The inner interpreter** (`STEP`) is written in the substrate.
3. **A temporary outer-interpreter seed** is written in the substrate. It is used only for a moment.
4. **Core words** are loaded through that seed.
5. **The seed is discarded.** The outer interpreter is now written in Forth words.

From then on the system is **Forth-in-Forth: metacircular and self-hosting.**

The design is in the spirit of the PDP-11. It uses clean registers, a flat memory model and orthogonal execution.

### 3. ITCH: the working project

📁 `itch/`

This is where new ideas are tested first. ITCH is an **optimized ITC** that aims to be even smaller, with more "biological" connections between its parts. It is the ground that will later grow into **ForthScript**.

Expect this part to change often.

---

## How to run

1. Clone or download the repository.
2. Open the `.html` file of the step you want in any modern browser (Chrome, Firefox, Safari, Edge).
3. You don't need to compile anything, run a server or install anything.

## Controls

* **Editor:** Write Forth in the editor and press **RUN**, or `Ctrl+Enter` / `Cmd+Enter`.
* **REPL:** Type commands directly at the `OK>` prompt.
* **EXPLORE:** Open the inspector to step through execution and watch the stacks.

---

## Philosophy

> Small enough to understand completely. Real enough to build anything.

* No standard is chased for its own sake. Only good ideas that remove noise are kept.
* A word becomes a primitive only if it really has to be one.
* Everything is written to be read, learned from and ported.
