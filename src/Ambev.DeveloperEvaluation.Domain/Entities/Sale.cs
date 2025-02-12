using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale in the system, including sale details, validation, and status management.
    /// </summary>
    public class Sale : BaseEntity, ISale
    {
        /// <summary>
        /// Gets or sets the sale number.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets the total amount for the sale, calculated from the items.
        /// </summary>
        public decimal TotalAmount { get; private set; } // Private set to prevent external modification

        /// <summary>
        /// Gets or sets the branch where the sale was made.
        /// </summary>
        public string BranchId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the customer associated with the sale.
        /// </summary>
        public string CustomerId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the sale is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the sale was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the sale was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets the unique identifier of the sale.
        /// </summary>
        string ISale.Id => Id.ToString();

        IEnumerable<ISaleItem> ISale.Items => Items;

        /// <summary>
        /// Private list to store sale items.
        /// </summary>
        private List<SaleItem> _items = new List<SaleItem>();

        /// <summary>
        /// Gets or sets the list of items in the sale.
        /// Whenever the items are updated, the total amount is recalculated.
        /// </summary>
        public List<SaleItem> Items
        {
            get => _items;
            set
            {
                _items = value ?? new List<SaleItem>();
                UpdateTotalAmount();
            }
        }

        /// <summary>
        /// Updates the total amount based on the sum of all items.
        /// </summary>
        private void UpdateTotalAmount()
        {
            TotalAmount = Items.Sum(item => item.Quantity * item.UnitPrice);
        }

        /// <summary>
        /// Adds an item to the sale and updates the total amount.
        /// </summary>
        public void AddItem(SaleItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _items.Add(item);
            UpdateTotalAmount();
        }

        /// <summary>
        /// Removes an item from the sale and updates the total amount.
        /// </summary>
        public void RemoveItem(SaleItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _items.Remove(item);
            UpdateTotalAmount();
        }

        /// <summary>
        /// Validates the sale using the SaleValidator rules.
        /// </summary>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        /// <summary>
        /// Cancels the sale by setting its status to cancelled.
        /// </summary>
        public void Cancel()
        {
            IsCancelled = true;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Activates the sale (sets IsCancelled to false).
        /// </summary>
        public void Activate()
        {
            IsCancelled = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
