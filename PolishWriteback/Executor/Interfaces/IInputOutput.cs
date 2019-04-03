namespace SmallScript.PolishWriteback.Executor.Interfaces
{
	public interface IInputOutput
	{
		int Read();

		void Write(int arg);
		
		void Write(string arg);
	}
}