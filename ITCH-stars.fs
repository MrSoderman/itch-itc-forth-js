\ ═══ STARS: four spinning stars bouncing over moving lines, translated to ITCH ═════════════════════
\ Paste into the program box and press RUN. STOP ends it.
\ Needs the GRAPHICS and MOTORS modules ( on in the standard system ).
\
\ What changed from the old version:
\  · Every raw address is a named VARIABLE now. The old code kept its state at addresses 0-39
\    ( 10 2 ! , 80 4 ! … ); in ITCH those are the machine's own registers: 2 is the compile stack's
\    depth, 4 >IN, 10 HERE, 12 the data stack's depth, 36 FRAMES. And ITCH's memory is bytes, with
\    2-byte cells, so addresses 2 and 3 would have been one cell.
\  · CASE has no OTHERWISE in ITCH: the default is simply the code before ENDCASE, with the value
\    still on the stack; ENDCASE drops it.
\  · LINE ( x0 y0 x1 y1 -- ) drew in the current colour: here it is SEGMENT, and COLOR takes the old
\    colour numbers 0-15 ( the C64's order ) from PALETTE.
\  · A bounce now keeps the star's speed: past the edge its velocity turns inward. The old code set it
\    to exactly 1 or -1, so a star slowed down after its first bounce. GLIDE is the one place to change
\    it back.
\  · FRAME runs 60 times a second as a motor ( ' word CODER ), as in the demos. The old RUN
\    ( BEGIN FRAME AGAIN ) had no frame to wait for; it would draw as fast as the machine runs.

ANEW STARS

\ ── colour ───────────────────────────────────────────────────────────────────────────────────────
CREATE PALETTE  $000 , $FFF , $F00 , $0FF , $F0F , $0F0 , $00F , $FF0 ,   \ the C64's 16 colours,
                $F80 , $840 , $F88 , $444 , $888 , $8F8 , $88F , $CCC ,   \ as $RGB
VARIABLE INK  $FFF INK !
: COLOR ( n -- ) 15 AND 2 * PALETTE + @ INK ! ;
: SEGMENT ( x0 y0 x1 y1 -- ) INK @ LINE ;

\ ── state ( the old addresses in brackets ) ───────────────────────────────────────────────────────
VARIABLE COL      0 COL !      \ [0]  the background's colour step
VARIABLE BG-OFF  10 BG-OFF !   \ [2]  the background lines' offset
VARIABLE ANGLE    0 ANGLE !    \ [36] the stars' rotation, 0-15
VARIABLE TICKS    0 TICKS !    \ [39] frames since the last turn
VARIABLE STAR-X  VARIABLE STAR-Y       \ [38] [37] the star being drawn ( SX SY and STEP are ITCH's own )
VARIABLE S1X  80 S1X !  VARIABLE S1Y  60 S1Y !  VARIABLE S1DX  3 S1DX !  VARIABLE S1DY  2 S1DY !
VARIABLE S2X 240 S2X !  VARIABLE S2Y  60 S2Y !  VARIABLE S2DX -2 S2DX !  VARIABLE S2DY  3 S2DY !
VARIABLE S3X  80 S3X !  VARIABLE S3Y 140 S3Y !  VARIABLE S3DX  2 S3DX !  VARIABLE S3DY -3 S3DY !
VARIABLE S4X 240 S4X !  VARIABLE S4Y 140 S4Y !  VARIABLE S4DX -3 S4DX !  VARIABLE S4DY -2 S4DY !

: MAP-COLOR ( n -- c )
  CASE
    0 OF 0 ENDOF    1 OF 11 ENDOF   2 OF 9 ENDOF    3 OF 2 ENDOF      \ black dark-grey brown red
    4 OF 8 ENDOF    5 OF 10 ENDOF   6 OF 7 ENDOF    7 OF 1 ENDOF      \ orange light-red yellow white
    8 OF 1 ENDOF    9 OF 7 ENDOF   10 OF 10 ENDOF  11 OF 8 ENDOF      \ and back again
   12 OF 2 ENDOF   13 OF 9 ENDOF   14 OF 11 ENDOF  15 OF 0 ENDOF
    0 SWAP                                   \ anything else: 0 ( ENDCASE drops the value )
  ENDCASE ;

: NC ( -- ) COL @ 1+ DUP 15 > IF DROP 0 THEN COL ! ;
: AC ( -- ) BG-OFF @ 1+ DUP 50 > IF DROP 0 THEN BG-OFF ! ;
: RC ( -- ) TICKS @ 1+ DUP 1 > IF                  \ turn one step every 2 frames
    DROP 0 TICKS !  ANGLE @ 1+ DUP 15 > IF DROP 0 THEN ANGLE !
  ELSE TICKS ! THEN ;

\ ── a star: four lines through ( STAR-X, STAR-Y ), in one of 16 turns ────────────────────────────────────
: X+ ( n -- x ) STAR-X @ + ;   : X- ( n -- x ) STAR-X @ SWAP - ;
: Y+ ( n -- y ) STAR-Y @ + ;   : Y- ( n -- y ) STAR-Y @ SWAP - ;
: DRAW-SPRITE ( x y -- )
  STAR-Y ! STAR-X !  1 COLOR
  ANGLE @ CASE
    0 OF  10 X- 0 Y+  10 X+ 0 Y+ SEGMENT   0 X+ 10 Y-  0 X+ 10 Y+ SEGMENT
          7 X- 7 Y-   7 X+ 7 Y+ SEGMENT    7 X- 7 Y+   7 X+ 7 Y-  SEGMENT  ENDOF
    1 OF  10 X- 4 Y-  10 X+ 4 Y+ SEGMENT   4 X- 10 Y-  4 X+ 10 Y+ SEGMENT
          8 X- 5 Y-   8 X+ 5 Y+ SEGMENT    5 X- 8 Y+   5 X+ 8 Y-  SEGMENT  ENDOF
    2 OF  9 X- 4 Y-   9 X+ 4 Y+ SEGMENT    4 X- 9 Y-   4 X+ 9 Y+  SEGMENT
          7 X- 2 Y-   7 X+ 2 Y+ SEGMENT    5 X- 5 Y+   5 X+ 5 Y-  SEGMENT  ENDOF
    3 OF  7 X- 7 Y-   7 X+ 7 Y+ SEGMENT    7 X- 7 Y+   7 X+ 7 Y-  SEGMENT
          10 X- 0 Y+  10 X+ 0 Y+ SEGMENT   0 X+ 10 Y-  0 X+ 10 Y+ SEGMENT  ENDOF
    4 OF  7 X- 7 Y-   7 X+ 7 Y+ SEGMENT    7 X- 7 Y+   7 X+ 7 Y-  SEGMENT
          10 X- 0 Y+  10 X+ 0 Y+ SEGMENT   0 X+ 10 Y-  0 X+ 10 Y+ SEGMENT  ENDOF
    5 OF  4 X- 9 Y-   4 X+ 9 Y+ SEGMENT    9 X- 4 Y-   9 X+ 4 Y+  SEGMENT
          5 X- 5 Y-   5 X+ 5 Y+ SEGMENT    7 X- 2 Y+   7 X+ 2 Y-  SEGMENT  ENDOF
    6 OF  4 X- 10 Y-  4 X+ 10 Y+ SEGMENT   10 X- 4 Y-  10 X+ 4 Y+ SEGMENT
          5 X- 8 Y-   5 X+ 8 Y+ SEGMENT    8 X- 5 Y+   8 X+ 5 Y-  SEGMENT  ENDOF
    7 OF  0 X+ 10 Y-  0 X+ 10 Y+ SEGMENT   10 X- 0 Y+  10 X+ 0 Y+ SEGMENT
          7 X- 7 Y+   7 X+ 7 Y- SEGMENT    7 X- 7 Y-   7 X+ 7 Y+  SEGMENT  ENDOF
    8 OF  0 X+ 10 Y-  0 X+ 10 Y+ SEGMENT   10 X- 0 Y+  10 X+ 0 Y+ SEGMENT
          7 X- 7 Y+   7 X+ 7 Y- SEGMENT    7 X- 7 Y-   7 X+ 7 Y+  SEGMENT  ENDOF
    9 OF  4 X+ 10 Y-  4 X- 10 Y+ SEGMENT   10 X+ 4 Y-  10 X- 4 Y+ SEGMENT
          5 X+ 8 Y-   5 X- 8 Y+ SEGMENT    8 X+ 5 Y+   8 X- 5 Y-  SEGMENT  ENDOF
   10 OF  9 X+ 4 Y-   9 X- 4 Y+ SEGMENT    4 X+ 9 Y-   4 X- 9 Y+  SEGMENT
          7 X+ 2 Y-   7 X- 2 Y+ SEGMENT    5 X+ 5 Y+   5 X- 5 Y-  SEGMENT  ENDOF
   11 OF  7 X+ 7 Y-   7 X- 7 Y+ SEGMENT    7 X+ 7 Y+   7 X- 7 Y-  SEGMENT
          10 X+ 0 Y+  10 X- 0 Y+ SEGMENT   0 X+ 10 Y+  0 X+ 10 Y- SEGMENT  ENDOF
   12 OF  7 X+ 7 Y-   7 X- 7 Y+ SEGMENT    7 X+ 7 Y+   7 X- 7 Y-  SEGMENT
          10 X+ 0 Y+  10 X- 0 Y+ SEGMENT   0 X+ 10 Y+  0 X+ 10 Y- SEGMENT  ENDOF
   13 OF  4 X+ 9 Y-   4 X- 9 Y+ SEGMENT    9 X+ 4 Y-   9 X- 4 Y+  SEGMENT
          5 X+ 5 Y-   5 X- 5 Y+ SEGMENT    7 X+ 2 Y+   7 X- 2 Y-  SEGMENT  ENDOF
   14 OF  10 X+ 4 Y-  10 X- 4 Y+ SEGMENT   4 X+ 10 Y-  4 X- 10 Y+ SEGMENT
          8 X+ 5 Y-   8 X- 5 Y+ SEGMENT    5 X+ 8 Y+   5 X- 8 Y-  SEGMENT  ENDOF
   15 OF  10 X+ 0 Y+  10 X- 0 Y+ SEGMENT   0 X+ 10 Y+  0 X+ 10 Y- SEGMENT
          7 X+ 7 Y+   7 X- 7 Y- SEGMENT    7 X+ 7 Y-   7 X- 7 Y+  SEGMENT  ENDOF
    \ anything else: turn 0 ( the value stays under the lines; ENDCASE drops it )
    10 X- 0 Y+  10 X+ 0 Y+ SEGMENT   0 X+ 10 Y-  0 X+ 10 Y+ SEGMENT
    7 X- 7 Y-   7 X+ 7 Y+ SEGMENT    7 X- 7 Y+   7 X+ 7 Y-  SEGMENT
  ENDCASE ;

\ ── moving: one step, bouncing off the edges ( 20 to max ) ────────────────────────────────────────
VARIABLE PA  VARIABLE VA
: GLIDE ( pos vel max -- ) >R VA ! PA !
  PA @ @ VA @ @ +
  DUP R@ > IF DROP R@  VA @ @ ABS NEGATE VA @ ! THEN    \ past the far edge: land on it, head back
  DUP 20 < IF DROP 20  VA @ @ ABS VA @ ! THEN           \ past the near edge: the same
  PA @ !  R> DROP ;
: UP1 ( -- ) S1X S1DX 300 GLIDE  S1Y S1DY 180 GLIDE ;
: UP2 ( -- ) S2X S2DX 300 GLIDE  S2Y S2DY 180 GLIDE ;
: UP3 ( -- ) S3X S3DX 300 GLIDE  S3Y S3DY 180 GLIDE ;
: UP4 ( -- ) S4X S4DX 300 GLIDE  S4Y S4DY 180 GLIDE ;

\ ── one frame ────────────────────────────────────────────────────────────────────────────────────
: FRAME ( -- )
  COL @ MAP-COLOR COLOR
  CLS
  0 BEGIN DUP 90 <= WHILE                    \ seven lines, sliding with BG-OFF
    DUP BG-OFF @ + >R
    160 R@ -  R@  160 R@ +  200 R@ -  SEGMENT
    R> DROP
    15 +
  REPEAT DROP
  S1X @ S1Y @ DRAW-SPRITE
  S2X @ S2Y @ DRAW-SPRITE
  S3X @ S3Y @ DRAW-SPRITE
  S4X @ S4Y @ DRAW-SPRITE
  NC AC RC
  UP1 UP2 UP3 UP4 ;
: FRAME-MOTOR ( m -- ) DROP FRAME ;

' FRAME-MOTOR CODER DROP                     \ 60 frames a second, as the demos do it
.( stars: STOP to end ) CR
