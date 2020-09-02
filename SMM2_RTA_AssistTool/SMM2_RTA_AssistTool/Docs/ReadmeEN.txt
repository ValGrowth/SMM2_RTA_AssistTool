========== ========== ==========
  SMMDeathCounter  Ver.2.3
========== ========== ==========

[Application]	SMMDeathCounter
[Author]		Val JP
[Environment]	Windows10
[Version]		2.3
[Last Updated]	Jan. 30, 2017

[Web]			https://twitter.com/
[E-mail]		val.niconico@gmail.com

---------- ----------
◇ Overview ◇
	
	
	It is a tool to count the number of death of Super Mario Maker automatically.
	This works for only playing mode and uploading mode.


◇ Environment ◇
	
	
	We're only able to confirm the operation under the following conditions.
	・Using the "AvarMedia GC550" on Windows10 PC.
	・Using the "AmaRecTV 3.10(: Japanese video capture application) on Windows10 PC.
	In other environment, I have not confirmed behavior.

	※When you access at the same time from some programs for a video and audio capture device,
	　is may causes some errors. (ex. in my environment, my OS was crushed.)
	　In this case, I recommend to use abstract capture device like a live function of AmaRecTV.


◇ How to use ◇
	
	(1) Run "SMMDeathCounter.exe"
	(2) Select a video device
	(3) Select a audio device
	(4) Play SMM, the number of death will increase
	(5) The number of death is save to the file "DeathCount.txt" in real time

	It is the specification that increasing of the number of death in the loading scene.
	If you don't like this, you can stop the increase of death by checking the Pause Button on right down of the window.


◇ Install ◇
	
	Run "SMMDeathCounter.exe".
	Installing is not required.
	And the registry is not used.


◇ Uninstall ◇
	
	Delete Directory "SMMDeathCounter".


◇ License ◇
	
	This software is free. It is allowed to do Personal use, or Commercial use.
	It is allowed to duplicate this software, and redistribute freely. However, it does not allowed to distribute what was modified.
	For any damage caused by this software, the author don't take responsiblity.

	In addition, this package contains the software "DirectShowLib-2005.dll" that is licensed based on the LGPL.
	You will be able to duplicate or re-distribute, modify it according to the LGPL.

◇ FAQ ◇

	Q. Can I change devices to capture?
	A. You can select devices by set [DefaultVideoDevice] and [DefaultAudioDevice] at Setting→Options.
	　 Or to disable [EnableDefaultDevice], you can always select devices when starting up.
