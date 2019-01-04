namespace OKHOSTING.Hosting.ERP
{
	/// <summary>
	/// An automated task that should be executed on a particular computer
	/// <para xml:lang="es">
	/// Una tarea automática que debe ser ejecutada en una computadora particular
	/// </para>
	/// </summary>
	public class ComputerTask: OKHOSTING.ERP.New.Production.AutomatedTask
	{
		/// <summary>
		/// Computer where this task must be executed
		/// <para xml:lang="es">
		/// Computadora en donde esta tarea debe ser ejecutada. 
		/// </para>
		/// </summary>
		public Computers.Computer Computer { get; set; }
	}
}