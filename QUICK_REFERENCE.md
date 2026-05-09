# MediClinic - Quick Reference Guide

## 🎯 Most Common Components

### Buttons
```html
<!-- Primary (use for main actions) -->
<button class="btn btn-primary">Save</button>

<!-- Outline (use for secondary actions) -->
<button class="btn btn-outline-primary">Cancel</button>

<!-- With Icon -->
<button class="btn btn-primary">
  <i class="fas fa-check me-2"></i>Confirm
</button>

<!-- Different Sizes -->
<button class="btn btn-primary btn-lg">Large</button>
<button class="btn btn-primary">Normal</button>
<button class="btn btn-primary btn-sm">Small</button>
```

### Cards
```html
<!-- Basic Card -->
<div class="card">
  <div class="card-header">Title</div>
  <div class="card-body">
    <p>Content</p>
  </div>
</div>

<!-- Card with Accent (pick one) -->
<div class="card card-primary"><!-- Blue border --></div>
<div class="card card-success"><!-- Green border --></div>
<div class="card card-warning"><!-- Yellow border --></div>
<div class="card card-danger"><!-- Red border --></div>
```

### Forms
```html
<div class="mb-3">
  <label for="input" class="form-label">Label</label>
  <input type="text" class="form-control" id="input" />
</div>

<!-- Select Dropdown -->
<select class="form-select">
  <option>Choose...</option>
  <option>Option 1</option>
</select>

<!-- Input Group (prefix) -->
<div class="input-group">
  <span class="input-group-text">$</span>
  <input type="text" class="form-control" />
</div>
```

### Alerts
```html
<div class="alert alert-primary">
  <i class="fas fa-info-circle me-2"></i>
  Info message
</div>

<div class="alert alert-success">
  <i class="fas fa-check-circle me-2"></i>
  Success message
</div>

<div class="alert alert-warning">
  <i class="fas fa-exclamation-triangle me-2"></i>
  Warning message
</div>

<div class="alert alert-danger">
  <i class="fas fa-times-circle me-2"></i>
  Error message
</div>
```

### Badges
```html
<span class="badge badge-primary">New</span>
<span class="badge badge-success">Confirmed</span>
<span class="badge badge-warning">Pending</span>
<span class="badge badge-danger">Cancelled</span>
```

### Tables
```html
<table class="table table-hover">
  <thead>
    <tr>
      <th>Column 1</th>
      <th>Column 2</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>Data 1</td>
      <td>Data 2</td>
    </tr>
  </tbody>
</table>
```

### Tile Grids (Statistics)
```html
<div class="tile-grid">
  <div class="stat-tile">
    <div class="stat-number">1,234</div>
    <div class="stat-label">Total Patients</div>
  </div>

  <div class="stat-tile">
    <div class="stat-number">456</div>
    <div class="stat-label">Appointments</div>
  </div>
</div>
```

### Feature Tiles
```html
<div class="row g-4">
  <div class="col-md-4">
    <div class="tile">
      <i class="fas fa-calendar-check tile-icon"></i>
      <div class="tile-title">Easy Booking</div>
      <div class="tile-subtitle">Schedule in seconds</div>
    </div>
  </div>
</div>
```

---

## 🎨 Utility Classes

### Text Colors
```html
<p class="text-clinic-primary">Blue text</p>
<p class="text-clinic-accent">Green text</p>
<p class="text-muted-light">Gray text</p>
<p class="text-white">White text</p>
```

### Spacing
```html
<!-- Margins -->
<div class="mt-3">Top margin</div>
<div class="mb-3">Bottom margin</div>
<div class="ms-3">Left margin</div>
<div class="me-3">Right margin</div>

<!-- Spacious (large) -->
<div class="mt-spacious">3rem top</div>
<div class="mb-spacious">3rem bottom</div>

<!-- Padding -->
<div class="p-3">All sides</div>
<div class="p-spacious">Large padding</div>
```

### Borders & Radius
```html
<div class="rounded-clinic">Large radius</div>
<div class="rounded-clinic-sm">Small radius</div>
<div class="border">Thin border</div>
<div class="border-primary">Colored border</div>
```

### Shadows
```html
<div class="shadow-clinic">Large shadow</div>
<div class="shadow-clinic-sm">Small shadow</div>
```

### Display & Flex
```html
<!-- Flexbox -->
<div class="d-flex">Flex container</div>
<div class="d-flex gap-3">With gap</div>
<div class="d-flex align-items-center">Vertically centered</div>
<div class="d-flex justify-content-between">Space between</div>

<!-- Text alignment -->
<div class="text-center">Centered</div>
<div class="text-start">Left aligned</div>
<div class="text-end">Right aligned</div>

<!-- Display types -->
<div class="d-block">Block element</div>
<div class="d-inline">Inline element</div>
<div class="d-none">Hidden</div>

<!-- Responsive hide/show -->
<div class="d-none d-md-block">Hidden on mobile, visible on tablet+</div>
<div class="d-lg-none">Hidden on large screens</div>
```

### Text Styles
```html
<p class="lead">Large introductory text</p>
<p><strong>Bold text</strong></p>
<p><em>Italic text</em></p>
<small>Small text</small>

<!-- Font weight -->
<p class="fw-bold">Bold</p>
<p class="fw-normal">Normal</p>
<p class="fw-light">Light</p>
```

---

## 📱 Responsive Grid

### Column System (Bootstrap 5)
```html
<!-- Full width on mobile, equal 2 columns on tablet+ -->
<div class="row">
  <div class="col-md-6">50% width on tablet+</div>
  <div class="col-md-6">50% width on tablet+</div>
</div>

<!-- 1 col mobile, 2 cols tablet, 3 cols desktop -->
<div class="row">
  <div class="col-md-6 col-lg-4">...</div>
  <div class="col-md-6 col-lg-4">...</div>
  <div class="col-md-6 col-lg-4">...</div>
</div>

<!-- Responsive gaps -->
<div class="row g-3">
  <div class="col-md-6">...</div>
  <div class="col-md-6">...</div>
</div>
```

---

## 🎬 Animations

### Fade In
```html
<div class="fade-in">Fades in smoothly</div>
```

### Pulse
```html
<div class="pulse">Pulses continuously</div>
```

---

## 🏥 Healthcare-Specific Examples

### Appointment Booking Form
```html
<div class="card">
  <div class="card-header">
    <h4><i class="fas fa-calendar-check me-2"></i>Book Appointment</h4>
  </div>
  <div class="card-body">
    <form>
      <div class="mb-3">
        <label class="form-label">Patient Name</label>
        <input type="text" class="form-control" />
      </div>

      <div class="mb-3">
        <label class="form-label">Doctor</label>
        <select class="form-select">
          <option>Dr. Sarah Johnson</option>
          <option>Dr. Mohammed Ali</option>
        </select>
      </div>

      <div class="mb-3">
        <label class="form-label">Date</label>
        <input type="date" class="form-control" />
      </div>

      <button type="submit" class="btn btn-primary w-100">
        <i class="fas fa-check me-2"></i>Book Appointment
      </button>
    </form>
  </div>
</div>
```

### Patient List
```html
<div class="card">
  <div class="card-header">
    <h5><i class="fas fa-users me-2"></i>Patient List</h5>
  </div>
  <div class="table-responsive">
    <table class="table table-hover mb-0">
      <thead>
        <tr>
          <th>Name</th>
          <th>Phone</th>
          <th>Last Visit</th>
          <th>Status</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>Ahmed Hassan</td>
          <td>+971 50 123 4567</td>
          <td>2 weeks ago</td>
          <td><span class="badge badge-success">Active</span></td>
        </tr>
      </tbody>
    </table>
  </div>
</div>
```

### Dashboard Stats
```html
<div class="hero-section mb-5">
  <h2>Welcome, Dr. Johnson</h2>
  <p>Here's your clinic overview</p>
</div>

<div class="tile-grid mb-5">
  <div class="stat-tile">
    <i class="fas fa-users tile-icon"></i>
    <div class="stat-number">42</div>
    <div class="stat-label">Today's Patients</div>
  </div>

  <div class="stat-tile">
    <i class="fas fa-calendar-alt tile-icon"></i>
    <div class="stat-number">15</div>
    <div class="stat-label">Pending Appointments</div>
  </div>

  <div class="stat-tile">
    <i class="fas fa-hourglass-half tile-icon"></i>
    <div class="stat-number">8</div>
    <div class="stat-label">In Progress</div>
  </div>

  <div class="stat-tile">
    <i class="fas fa-check-circle tile-icon"></i>
    <div class="stat-number">456</div>
    <div class="stat-label">Completed Today</div>
  </div>
</div>
```

---

## 📚 Available Font Awesome Icons

### Common Healthcare Icons
```html
<i class="fas fa-hospital-user"></i>      <!-- Hospital User -->
<i class="fas fa-user-md"></i>             <!-- Doctor -->
<i class="fas fa-calendar-check"></i>      <!-- Appointment -->
<i class="fas fa-heartbeat"></i>           <!-- Heartbeat -->
<i class="fas fa-prescription-bottle"></i> <!-- Prescription -->
<i class="fas fa-pills"></i>               <!-- Pills -->
<i class="fas fa-stethoscope"></i>         <!-- Stethoscope -->
<i class="fas fa-first-aid-kit"></i>       <!-- First Aid -->
<i class="fas fa-ambulance"></i>           <!-- Ambulance -->
<i class="fas fa-hospital"></i>            <!-- Hospital -->
```

### Action Icons
```html
<i class="fas fa-save"></i>                <!-- Save -->
<i class="fas fa-edit"></i>                <!-- Edit -->
<i class="fas fa-trash"></i>               <!-- Delete -->
<i class="fas fa-check"></i>               <!-- Checkmark -->
<i class="fas fa-times"></i>               <!-- X/Close -->
<i class="fas fa-download"></i>            <!-- Download -->
<i class="fas fa-print"></i>               <!-- Print -->
<i class="fas fa-share"></i>               <!-- Share -->
```

### Status Icons
```html
<i class="fas fa-check-circle"></i>        <!-- Confirmed -->
<i class="fas fa-hourglass-half"></i>      <!-- Pending -->
<i class="fas fa-times-circle"></i>        <!-- Cancelled -->
<i class="fas fa-info-circle"></i>         <!-- Info -->
<i class="fas fa-exclamation-triangle"></i><!-- Warning -->
```

---

## 🎨 Color Variables Quick Access

```css
/* In any CSS or style block, use: */
background: var(--clinic-primary);        /* #0b6efd */
background: var(--clinic-accent);         /* #00b894 */
background: var(--clinic-success);        /* #28a745 */
background: var(--clinic-warning);        /* #ffc107 */
background: var(--clinic-danger);         /* #dc3545 */
color: var(--clinic-text);                /* #222 */
color: var(--clinic-muted);               /* #6c757d */
```

---

## 🚀 Common Page Layouts

### 2-Column Layout
```html
<div class="row g-4">
  <div class="col-lg-8">
    <!-- Main content -->
  </div>
  <div class="col-lg-4">
    <!-- Sidebar -->
  </div>
</div>
```

### 3-Column Layout
```html
<div class="row g-4">
  <div class="col-lg-4">Column 1</div>
  <div class="col-lg-4">Column 2</div>
  <div class="col-lg-4">Column 3</div>
</div>
```

### Full-width with Sidebar
```html
<div class="row g-4">
  <div class="col-md-3">
    <!-- Sidebar navigation -->
  </div>
  <div class="col-md-9">
    <!-- Main content -->
  </div>
</div>
```

---

## ⌨️ Keyboard Shortcuts in DevTools

| Key | Action |
|-----|--------|
| F12 | Open DevTools |
| Ctrl+Shift+C | Select element |
| Ctrl+Shift+K | Open Console |
| Tab | Navigate through elements |

---

## 🔗 Useful Links

| Resource | Link |
|----------|------|
| Bootstrap Docs | https://getbootstrap.com/docs/5.0/ |
| Font Awesome | https://fontawesome.com/icons |
| Color Picker | https://htmlcolorcodes.com/ |
| Accessibility | https://www.w3.org/WAI/WCAG21/quickref/ |

---

**Happy Building! 🎉**

For detailed component examples, visit: `/components`  
For full documentation, see: `DESIGN_SYSTEM.md`
