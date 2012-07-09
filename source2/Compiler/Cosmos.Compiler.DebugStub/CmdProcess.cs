using System;
using System.Linq;
using Cosmos.Assembler;
using Cosmos.Assembler.x86;

namespace Cosmos.Debug.DebugStub {
	public class CmdProcess : Cosmos.Assembler.Code {

		public CmdProcess(Assembler.Assembler aAssembler) : base(aAssembler) {}

		public override void Assemble() {
			new Comment("X#: Group DebugStub");

			new Comment("X#: const Ds2Vs_Noop = 0");
			new LiteralAssemblerCode("DebugStub_Const_Ds2Vs_Noop equ 0");

			new Comment("X#: const Ds2Vs_TracePoint = 1");
			new LiteralAssemblerCode("DebugStub_Const_Ds2Vs_TracePoint equ 1");

			new Comment("X#: const Ds2Vs_Message = 2");
			new LiteralAssemblerCode("DebugStub_Const_Ds2Vs_Message equ 2");

			new Comment("X#: const Ds2Vs_BreakPoint = 3");
			new LiteralAssemblerCode("DebugStub_Const_Ds2Vs_BreakPoint equ 3");

			new Comment("X#: const Ds2Vs_Error = 4");
			new LiteralAssemblerCode("DebugStub_Const_Ds2Vs_Error equ 4");

			new Comment("X#: const Ds2Vs_Pointer = 5");
			new LiteralAssemblerCode("DebugStub_Const_Ds2Vs_Pointer equ 5");

			new Comment("X#: const Ds2Vs_Started = 6");
			new LiteralAssemblerCode("DebugStub_Const_Ds2Vs_Started equ 6");

			new Comment("X#: const Ds2Vs_MethodContext = 7");
			new LiteralAssemblerCode("DebugStub_Const_Ds2Vs_MethodContext equ 7");

			new Comment("X#: const Ds2Vs_MemoryData = 8");
			new LiteralAssemblerCode("DebugStub_Const_Ds2Vs_MemoryData equ 8");

			new Comment("X#: const Ds2Vs_CmdCompleted = 9");
			new LiteralAssemblerCode("DebugStub_Const_Ds2Vs_CmdCompleted equ 9");

			new Comment("X#: const Ds2Vs_Registers = 10");
			new LiteralAssemblerCode("DebugStub_Const_Ds2Vs_Registers equ 10");

			new Comment("X#: const Ds2Vs_Frame = 11");
			new LiteralAssemblerCode("DebugStub_Const_Ds2Vs_Frame equ 11");

			new Comment("X#: const Ds2Vs_Stack = 12");
			new LiteralAssemblerCode("DebugStub_Const_Ds2Vs_Stack equ 12");

			new Comment("X#: const Ds2Vs_Pong = 13");
			new LiteralAssemblerCode("DebugStub_Const_Ds2Vs_Pong equ 13");

			new Comment("X#: procedure AckCommand {");
			new LiteralAssemblerCode("DebugStub_AckCommand:");

			new LiteralAssemblerCode("; We acknowledge receipt of the command AND the processing of it.");

			new LiteralAssemblerCode("; -In the past the ACK only acknowledged receipt.");

			new LiteralAssemblerCode("; We have to do this because sometimes callers do more processing.");

			new LiteralAssemblerCode("; We ACK even ones we dont process here, but do not ACK Noop.");

			new LiteralAssemblerCode("; The buffers should be ok because more wont be sent till after our NACK");

			new LiteralAssemblerCode("; is received.");

			new LiteralAssemblerCode("; Right now our max cmd size is 2 (Cmd + Cmd ID) + 5 (Data) = 7.");

			new LiteralAssemblerCode("; UART buffer is 16.");

			new LiteralAssemblerCode("; We may need to revisit this in the future to ack not commands, but data chunks");

			new LiteralAssemblerCode("; and move them to a buffer.");

			new LiteralAssemblerCode("; The buffer problem exists only to inbound data, not outbound data (relative to DebugStub).");

			new Comment("X#: AL = #Ds2Vs_CmdCompleted");
			new LiteralAssemblerCode("Mov AL, DebugStub_Const_Ds2Vs_CmdCompleted");

			new Comment("X#: ComWriteAL()");
			new LiteralAssemblerCode("Call DebugStub_ComWriteAL");

			new Comment("X#: EAX = .CommandID");
			new LiteralAssemblerCode("Mov EAX, [DebugStub_CommandID]");

			new Comment("X#: ComWriteAL()");
			new LiteralAssemblerCode("Call DebugStub_ComWriteAL");

			new Comment("X#: }");
			new LiteralAssemblerCode("DebugStub_AckCommand_Exit:");
			new LiteralAssemblerCode("Ret");

			new Comment("X#: procedure ProcessCommandBatch {");
			new LiteralAssemblerCode("DebugStub_ProcessCommandBatch:");

			new Comment("X#: Begin:");
			new LiteralAssemblerCode("DebugStub_ProcessCommandBatch_Begin:");

			new Comment("X#: ProcessCommand()");
			new LiteralAssemblerCode("Call DebugStub_ProcessCommand");

			new LiteralAssemblerCode("; See if batch is complete");

			new LiteralAssemblerCode("; Loop and wait");

			new LiteralAssemblerCode("; Vs2Ds.BatchEnd");

			new Comment("X#: if AL != 8 goto Begin");
			new Compare { DestinationReg = RegistersEnum.AL, SourceValue = 8 };
			new ConditionalJump { Condition = ConditionalTestEnum.NotZero, DestinationLabel = "DebugStub_ProcessCommandBatch_Begin" };

			new Comment("X#: AckCommand()");
			new LiteralAssemblerCode("Call DebugStub_AckCommand");

			new Comment("X#: }");
			new LiteralAssemblerCode("DebugStub_ProcessCommandBatch_Exit:");
			new LiteralAssemblerCode("Ret");

		}
	}
}
