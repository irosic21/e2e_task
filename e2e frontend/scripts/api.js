const apiUrl = "https://localhost:7149/api/Employee";

// Fetch all employees
async function fetchEmployees() {
    try {
        console.log("Fetching employees...");
        const response = await fetch(apiUrl);
        if (!response.ok) throw new Error(`Failed to fetch employees: ${response.status}`);

        const data = await response.json();
        console.log("Employees received:", data);
        return data;
    } catch (error) {
        console.error("Error fetching employees:", error);
        return [];
    }
}

// Fetch employee by ID
async function fetchEmployeeById(id) {
    try {
        console.log(`Fetching employee details for ID: ${id}`);
        const response = await fetch(`${apiUrl}/${id}`);
        if (!response.ok) throw new Error(`Failed to fetch employee: ${response.status}`);

        const data = await response.json();
        console.log("Employee details:", data);
        return data;
    } catch (error) {
        console.error("Error fetching employee details:", error);
        return null;
    }
}

// Add or update an employee
async function submitEmployeeForm(formData, isEdit, id) {
    const endpoint = isEdit ? `${apiUrl}/update/${id}` : `${apiUrl}/add`;
    const method = isEdit ? "PUT" : "POST";

    try {
        console.log(`Submitting ${isEdit ? "update" : "new"} employee form...`);
        const response = await fetch(endpoint, { method, body: formData });
        if (!response.ok) {
            const errorText = await response.text();
            console.error("Server error:", errorText);
            throw new Error(`Failed to submit employee form: ${errorText}`);
        }

        console.log("Employee successfully added/updated.");
        return true;
    } catch (error) {
        console.error("Error submitting employee form:", error);
        return false;
    }
}

// Delete employee
async function deleteEmployee(id) {
    try {
        console.log(`Deleting employee with ID: ${id}`);
        const response = await fetch(`${apiUrl}/delete/${id}`, { method: "DELETE" });
        if (!response.ok) throw new Error("Failed to delete employee");

        console.log("Employee deleted successfully.");
        return true;
    } catch (error) {
        console.error("Error deleting employee:", error);
        return false;
    }
}
