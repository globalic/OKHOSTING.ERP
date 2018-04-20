namespace OKHOSTING.Hosting.ERP
{
    /// <summary>
    /// An automated task that should be executed on a particular computer
    /// </summary>
    public class ComputerTask: OKHOSTING.ERP.New.Production.AutomatedTask
    {
        /// <summary>
        /// Computer where this task must be executed
        /// </summary>
        public Computers.Computer Computer { get; set; }
    }
}