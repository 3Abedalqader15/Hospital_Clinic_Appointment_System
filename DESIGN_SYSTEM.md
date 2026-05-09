# MediClinic Design System
## A Modern Hospital Appointment Management Interface

---

## 📋 Overview

The **MediClinic Design System** is a comprehensive, accessible, and responsive frontend framework built with Bootstrap 5 and custom CSS. It's optimized for healthcare applications with a focus on user experience, accessibility, and modern aesthetics.

### Key Features
- ✅ **Responsive Design**: Mobile-first approach (works on all devices)
- ✅ **Dark Mode Support**: Automatic dark theme detection
- ✅ **Accessible**: WCAG 2.1 AA compliant
- ✅ **Modern Aesthetics**: Clean, professional healthcare branding
- ✅ **Performance**: Optimized animations and transitions
- ✅ **Component Library**: Pre-built, reusable components

---

## 🎨 Color Palette

### Primary Colors
| Color | Variable | Usage |
|-------|----------|-------|
| Blue | `--clinic-primary: #0b6efd` | Buttons, links, primary actions |
| Dark Blue | `--clinic-primary-dark: #095ed6` | Hover states, emphasis |
| Green | `--clinic-accent: #00b894` | Success, positive actions |
| Light Green | `--clinic-accent-light: #4cd964` | Alternative accent |

### Status Colors
| Status | Color | Variable |
|--------|-------|----------|
| Success | Green | `--clinic-success: #28a745` |
| Warning | Yellow | `--clinic-warning: #ffc107` |
| Danger | Red | `--clinic-danger: #dc3545` |
| Info | Cyan | `--clinic-info: #17a2b8` |

### Neutral Colors
| Type | Light | Dark |
|------|-------|------|
| Text | `#222` | `#e6eef8` |
| Muted | `#6c757d` | `#9aa6b2` |
| Background | `#f8f9fb` | `#0b0f14` |
| Surface | `#eef3fb` | `#091018` |

---

## 🔤 Typography

### Font Stack
```css
-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif
```

### Heading Hierarchy
| Level | Size (Desktop) | Weight | Usage |
|-------|---|---|---|
| H1 | 2.5rem | 700 | Page titles |
| H2 | 2rem | 700 | Section headers |
| H3 | 1.5rem | 700 | Subsections |
| H4 | 1.25rem | 700 | Sub-subsections |
| H5 | 1.1rem | 700 | Minor headings |
| H6 | 1rem | 700 | Labels |

### Body Text
- **Default**: 1rem / 1.6 line height
- **Lead**: 1.15rem, weight 500 (introductory text)
- **Small**: 0.875rem
- **Muted**: Uses `--clinic-text-light` color

---

## 🧩 Components

### Buttons

#### Primary Button
```html
<button class="btn btn-primary">Primary Action</button>
```
- **Background**: Linear gradient (blue to dark blue)
- **Hover**: Lifts up (-2px) with enhanced shadow
- **Usage**: Main CTAs, form submission

#### Secondary Button
```html
<button class="btn btn-secondary">Secondary</button>
```
- **Background**: Surface color
- **Border**: Primary color
- **Usage**: Alternative actions

#### Outline Button
```html
<button class="btn btn-outline-primary">Outline</button>
```
- **Border**: Primary color
- **Hover**: Fills with primary background
- **Usage**: Less prominent actions

#### Button Sizes
```html
<button class="btn btn-primary btn-lg">Large Button</button>
<button class="btn btn-primary">Normal Button</button>
<button class="btn btn-primary btn-sm">Small Button</button>
```

### Cards

#### Basic Card
```html
<div class="card">
  <div class="card-header">Card Title</div>
  <div class="card-body">
    <h5 class="card-title">Title</h5>
    <p class="card-text">Content here</p>
  </div>
</div>
```

#### Colored Card (Primary)
```html
<div class="card card-primary">
  <div class="card-body">
    <p>Content</p>
  </div>
</div>
```
- **Variants**: `card-primary`, `card-success`, `card-warning`, `card-danger`
- **Left Border**: 4px colored accent

### Forms

#### Text Input
```html
<div class="mb-3">
  <label for="example" class="form-label">Label</label>
  <input type="text" class="form-control" id="example" />
</div>
```

#### Form Select
```html
<div class="mb-3">
  <label for="select" class="form-label">Choose Option</label>
  <select class="form-select" id="select">
    <option>Option 1</option>
    <option>Option 2</option>
  </select>
</div>
```

#### Input Group
```html
<div class="input-group mb-3">
  <span class="input-group-text">$</span>
  <input type="text" class="form-control" />
</div>
```

### Tables

#### Standard Table
```html
<table class="table table-hover">
  <thead>
    <tr>
      <th>Header 1</th>
      <th>Header 2</th>
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

- **Features**: Hover effect, striped rows, clean styling
- **Responsive**: Scrollable on mobile

### Alerts & Badges

#### Alert
```html
<div class="alert alert-primary">
  <strong>Note:</strong> Alert message here
</div>
```
- **Variants**: `alert-primary`, `alert-success`, `alert-warning`, `alert-danger`

#### Badge
```html
<span class="badge badge-primary">New</span>
<span class="badge badge-success">Confirmed</span>
```

### Hero Section

```html
<section class="hero-section">
  <h1>Welcome to MediClinic</h1>
  <p>Your modern appointment management system</p>
  <button class="btn btn-primary btn-lg">Get Started</button>
</section>
```

### Tile Grid

```html
<div class="tile-grid">
  <div class="tile">
    <i class="fas fa-calendar-check tile-icon"></i>
    <div class="tile-title">Appointments</div>
    <div class="tile-subtitle">Manage bookings</div>
  </div>

  <div class="stat-tile">
    <div class="stat-number">1,234</div>
    <div class="stat-label">Total Patients</div>
  </div>
</div>
```

- **Responsive**: 1 col (mobile) → 2 cols (tablet) → 4 cols (desktop)
- **Hover**: Lifts up on hover

---

## 🎯 Spacing System

### Margin & Padding Scale
```css
.m-1 / .p-1   /* 0.25rem */
.m-2 / .p-2   /* 0.5rem */
.m-3 / .p-3   /* 1rem */
.m-4 / .p-4   /* 1.5rem */
.m-5 / .p-5   /* 3rem */

/* Spacious variants */
.mt-spacious   /* margin-top: 3rem */
.mb-spacious   /* margin-bottom: 3rem */
```

---

## 🌑 Dark Mode

The design system includes automatic dark mode support using `prefers-color-scheme: dark`.

### Enabling Dark Mode
Dark mode is automatically applied if the user's system preference is set to dark.

### Testing Dark Mode
**Firefox**: Right-click → Inspect → Console → `window.matchMedia('(prefers-color-scheme: dark)').matches`

**Chrome DevTools**: Click ⋮ → More tools → Rendering → Scroll to "Emulate CSS media feature prefers-color-scheme"

---

## ♿ Accessibility Features

### WCAG 2.1 AA Compliance
- ✅ **Color Contrast**: All text meets WCAG AA standards
- ✅ **Focus Indicators**: Clear 2px outline on focused elements
- ✅ **Semantic HTML**: Proper heading hierarchy, alt text for images
- ✅ **Keyboard Navigation**: All interactive elements are keyboard accessible
- ✅ **Screen Reader Support**: Proper ARIA labels and semantic markup

### Focus States
All interactive elements have visible focus indicators:
```css
:focus-visible {
  outline: 2px solid var(--clinic-primary);
  outline-offset: 2px;
}
```

### Skip Link
Include at the top of your page to improve keyboard navigation:
```html
<a href="#main-content" class="skip-link">Skip to main content</a>
```

---

## 📱 Responsive Breakpoints

| Breakpoint | Width | Usage |
|-----------|-------|-------|
| Mobile | < 576px | Small phones |
| Tablet | 576px - 768px | Tablets |
| Desktop | 768px - 992px | Small laptops |
| Large Desktop | ≥ 992px | Large screens |

### Mobile-First Approach
```html
<!-- Default: mobile (1 column) -->
<div class="tile-grid">
  <!-- Becomes 2 columns at 576px -->
  <!-- Becomes 3 columns at 768px -->
  <!-- Becomes 4 columns at 992px -->
</div>
```

---

## 🎬 Animations & Transitions

### Transition Speed
- **Fast**: `--clinic-transition: 180ms` (UI feedback)
- **Slow**: `--clinic-transition-slow: 300ms` (subtle animations)

### Built-in Animations

#### Fade In
```html
<div class="fade-in">Content fades in</div>
```

#### Pulse
```html
<div class="pulse">Content pulses</div>
```

---

## 📦 Navbar Component

```html
<nav class="navbar navbar-expand-lg navbar-light navbar-clinic sticky-top">
  <div class="container">
    <a class="navbar-brand" href="#">
      <i class="fas fa-hospital-user me-2 brand-icon"></i>
      <span class="fw-bold">MediClinic</span>
    </a>

    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" 
            data-bs-target=".navbar-collapse">
      <span class="navbar-toggler-icon"></span>
    </button>

    <div class="navbar-collapse collapse">
      <ul class="navbar-nav ms-auto">
        <li class="nav-item">
          <a class="nav-link" href="#">Home</a>
        </li>
      </ul>
    </div>
  </div>
</nav>
```

- **Features**: Sticky positioning, smooth underline animation on hover
- **Responsive**: Collapses to hamburger menu on mobile

---

## 🔲 Footer Component

```html
<footer class="footer-clinic">
  <div class="container">
    <div class="row py-4">
      <div class="col-md-4">
        <h6 class="text-white fw-bold">MediClinic</h6>
        <p class="text-muted-light">Quality healthcare services</p>
      </div>
    </div>
  </div>
</footer>
```

- **Background**: Dark gradient
- **Text**: White headings, muted light text
- **Links**: Hover turns primary blue

---

## 🛠️ Utility Classes

### Text
```html
<p class="text-clinic-primary">Blue text</p>
<p class="text-clinic-accent">Green text</p>
<p class="text-muted-light">Muted gray text</p>
```

### Shadows
```html
<div class="shadow-clinic">Large shadow</div>
<div class="shadow-clinic-sm">Small shadow</div>
```

### Borders & Radius
```html
<div class="rounded-clinic">Large radius (16px)</div>
<div class="rounded-clinic-sm">Small radius (6px)</div>
```

### Display & Spacing
```html
<div class="d-flex align-items-center gap-3">Flex container</div>
<div class="text-center">Centered text</div>
```

---

## 📋 Usage Examples

### Dashboard Card
```html
<div class="card">
  <div class="card-header">
    <h5 class="mb-0">Patient Overview</h5>
  </div>
  <div class="card-body">
    <div class="row">
      <div class="col-md-6">
        <div class="stat-tile">
          <div class="stat-number">42</div>
          <div class="stat-label">Active Appointments</div>
        </div>
      </div>
    </div>
  </div>
</div>
```

### Form Section
```html
<div class="card">
  <div class="card-header">
    <h4>Book Appointment</h4>
  </div>
  <div class="card-body">
    <form>
      <div class="mb-3">
        <label class="form-label">Patient Name</label>
        <input type="text" class="form-control" />
      </div>

      <button type="submit" class="btn btn-primary">Submit</button>
    </form>
  </div>
</form>
```

---

## 🔗 External Resources

- **Bootstrap Documentation**: https://getbootstrap.com/docs/5.0/
- **Font Awesome Icons**: https://fontawesome.com/
- **Accessibility Guidelines**: https://www.w3.org/WAI/WCAG21/quickref/

---

## 📝 CSS Custom Properties Reference

```css
/* Colors */
--clinic-primary
--clinic-primary-dark
--clinic-accent
--clinic-muted
--clinic-text
--clinic-text-light
--clinic-bg
--clinic-card-bg
--clinic-surface

/* Spacing */
--clinic-radius
--clinic-radius-lg
--clinic-radius-sm

/* Shadows */
--clinic-shadow-xs
--clinic-shadow-sm
--clinic-shadow-md
--clinic-shadow-lg

/* Transitions */
--clinic-transition
--clinic-transition-slow

/* Typography */
--clinic-font-family
--clinic-font-mono
```

---

## 🚀 Next Steps

1. **Customize Colors**: Update CSS variables in `:root` for branding
2. **Add Content**: Replace placeholders with real appointment/patient data
3. **Integrate Backend**: Connect forms to your C# backend
4. **Test Accessibility**: Use tools like Axe DevTools
5. **Deploy**: Optimize images and bundle CSS/JS

---

## 💡 Tips

- Use **Font Awesome icons** for visual consistency
- Keep **line-height** at 1.6 for readability
- Test **dark mode** on actual user devices
- Use **semantic HTML** for better SEO
- Add **loading states** to buttons with disabled attribute
- Include **error messages** with danger color badges

---

**Version**: 1.0.0  
**Last Updated**: 2026  
**Maintained by**: MediClinic Development Team
